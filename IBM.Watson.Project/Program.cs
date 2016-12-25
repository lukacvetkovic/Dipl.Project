using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using IBM.Watson.Project.Database;
using IBM.Watson.Project.Helpers;
using IBM.Watson.Project.Helpers.PDFParser;
using IBM.Watson.Project.Helpers.WatsonHelper;

namespace IBM.Watson.Project
{
    class Program
    {

        //private static readonly string APIKey = "ea4cb11ced2395985e21914549b176c68111faac";
        private static readonly string APIKey = "aebf5e5def1d9ac9f0334cb1d8ef9d7ad477c314";
        static void Main(string[] args)
        {
            ExportWatsonKeywords();
            ExportKeywordUnion();
            ExportConcepts();
            ExportKeywords();
            ExportStatistics();
            return;
            List<string> errors = new List<string>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Articles";

            string[] files = Directory.GetFiles(path, "*.pdf", SearchOption.AllDirectories);

            foreach (var fileName in files)
            {
                try
                {



                    Console.WriteLine(fileName);

                    IPDFParser pdfParser = new ITextSharpPDFParser();

                    var text = pdfParser.ExtractTextFromPdf(fileName);

                    var name = fileName.Replace("C:\\Users\\Luka\\Desktop\\Articles\\", "");
                    name = name.Replace(".pdf", "");

                    Regex regex = new Regex(@"Keywords: ([^\)]*).\n1.");

                    IWatsonHelper helper = new WatsonHelperText(APIKey);
                    Match match = regex.Match(text);

                    string keywordsFromArticleString = "";

                    if (match.Success)
                    {
                        keywordsFromArticleString = match.Groups[1].Value;
                        keywordsFromArticleString = keywordsFromArticleString.Replace("-\n", "");
                        keywordsFromArticleString = keywordsFromArticleString.Replace("\n", "");
                    }
                    else
                    {
                        Console.WriteLine("Keywords not found");
                        errors.Add("Keywords not found for filename: " + fileName);
                        continue;
                    }

                    var result = helper.GetResult(text);
                    //Console.WriteLine(JSONHelper.Serialize(result));

                    string[] keywordsFromArticle = keywordsFromArticleString.Split(',');

                    Console.WriteLine("-- ------------------------------- --");
                    Console.WriteLine("-- ----------Keywords------------- --");
                    Console.WriteLine();

                    foreach (var keyword in keywordsFromArticle)
                    {
                        Console.WriteLine(keyword.Trim());
                    }


                    Console.WriteLine();
                    Console.WriteLine("-- ------------------------------- --");
                    Console.WriteLine("-- -----------WatsonKeywords------ --");
                    Console.WriteLine();

                    foreach (var keyword in result.keywords.OrderBy(p => p.text))
                    {
                        Console.WriteLine(keyword.text);
                    }

                    Console.WriteLine();
                    Console.WriteLine("-- ------------------------------- --");
                    Console.WriteLine("-- ------------Concepts----------- --");
                    Console.WriteLine();

                    foreach (var concept in result.concepts.OrderBy(p => p.text))
                    {
                        Console.WriteLine(concept.text);
                    }

                    SaveToDb(name, keywordsFromArticle.ToList(), result.keywords.Select(p => p.text).ToList(),
                        result.concepts.Select(p => p.text).ToList());

                    System.Threading.Thread.Sleep(5000);
                }
                catch (Exception e)
                {

                    using (StreamWriter writetext = new StreamWriter("exception.txt"))
                    {

                        writetext.WriteLine(e.StackTrace.ToString());


                    }

                    System.Threading.Thread.Sleep(10000);
                }
            }

            //IWatsonHelper helpUrl = new WatsonHelperUrl(APIKey);

            //var resultUrl= helpUrl.GetResult("http://www.baynews9.com/alerts.html");

            //Console.WriteLine(JSONHelper.Serialize(result));
            Console.WriteLine("Done");
            using (StreamWriter writetext = new StreamWriter("error.txt"))
            {
                foreach (var error in errors)
                {
                    writetext.WriteLine(error);
                }

            }

            Console.ReadLine();
        }

        private static void ExportStatistics()
        {
            DiplProjectDb db = new DiplProjectDb();

            var articles = db.Article.ToList();

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<List<string>> statisticsList = new List<List<string>>();

            List<string> firstRow= new List<string>() {"Id","Name","Article keyword count","Watson count", "Watson matched (%)"};

            statisticsList.Add(firstRow);

            foreach (var article in articles.OrderBy(p => p.Id))
            {
                List<string> row = new List<string>() { article.Id.ToString(),article.ArticleName,article.ArticleKeyword.Count.ToString(),article.WatsonKeyword.Count.ToString()};
                int articleKeywordCount = article.ArticleKeyword.Count;

                int matched = 0;

                var watsonKeywords = article.WatsonKeyword.Select(p => p.Keyword);

                foreach (var articleKeyword in article.ArticleKeyword.Select(p=>p.Keyword))
                {
                    if (watsonKeywords.Contains(articleKeyword))
                    {
                        matched++;
                    }
                }
                row.Add(((double)matched/articleKeywordCount).ToString());

                statisticsList.Add(row);

            }

            CSVHelper.ExporToCSV(pathDesktop, "statistics.csv", statisticsList);
        }

        private static void ExportKeywords()
        {
            DiplProjectDb db = new DiplProjectDb();

            var articles = db.Article.ToList();

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<List<string>> keyWordList = new List<List<string>>();

            foreach (var article in articles.OrderBy(p => p.Id))
            {
                List<string> row = new List<string>();
                row.Add(article.Id.ToString());
                row.Add(article.ArticleName);

                foreach (var watsonKeyword in article.ArticleKeyword)
                {
                    row.Add(watsonKeyword.Keyword);
                }

                keyWordList.Add(row);
            }

            CSVHelper.ExporToCSV(pathDesktop, "keywords.csv", keyWordList);
        }

        private static void ExportConcepts()
        {
            DiplProjectDb db = new DiplProjectDb();

            var articles = db.Article.ToList();

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<List<string>> conceptList = new List<List<string>>();

            foreach (var article in articles.OrderBy(p => p.Id))
            {
                List<string> row = new List<string>();
                row.Add(article.Id.ToString());
                row.Add(article.ArticleName);

                foreach (var concept in article.Concept)
                {
                    row.Add(concept.ConceptName);
                }

                conceptList.Add(row);
            }

            CSVHelper.ExporToCSV(pathDesktop, "concept.csv", conceptList);
        }

        private static void ExportKeywordUnion()
        {
            DiplProjectDb db = new DiplProjectDb();

            var articles = db.Article.ToList();

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<List<string>> exportList = new List<List<string>>();

            var watsonKeywords = db.WatsonKeyword.ToList();
            var articleKeywords = db.ArticleKeyword.ToList();

            var keyWordUnion =
                watsonKeywords.Select(p => p.Keyword).ToList().Union(articleKeywords.Select(x => x.Keyword).ToList());
            keyWordUnion = keyWordUnion.Distinct().OrderBy(p => p.ToString().Trim()).ToList();

            List<string> firstRow = new List<string>() { "Id", "Name" };

            foreach (var keyWord in keyWordUnion)
            {
                firstRow.Add(keyWord);
            }

            exportList.Add(firstRow);

            foreach (var article in articles.OrderBy(p => p.Id))
            {
                List<string> row = new List<string> { article.Id.ToString(), article.ArticleName };

                var articleUnionKeyWord =
                    article.ArticleKeyword.Select(p => p.Keyword)
                    .Union(article.WatsonKeyword.Select(p => p.Keyword.Trim()))
                    .Distinct()
                    .ToList();

                foreach (var keyWord in keyWordUnion)
                {
                    row.Add(articleUnionKeyWord.Contains(keyWord) ? "1" : "0");
                }

                exportList.Add(row);
            }



            CSVHelper.ExporToCSV(pathDesktop, "keywordUnion.csv", exportList);

        }

        private static void ExportWatsonKeywords()
        {
            DiplProjectDb db = new DiplProjectDb();

            var articles = db.Article.ToList();

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<List<string>> foundKeyWordList = new List<List<string>>();

            foreach (var article in articles.OrderBy(p => p.Id))
            {
                List<string> row = new List<string>();
                row.Add(article.Id.ToString());
                row.Add(article.ArticleName);

                foreach (var watsonKeyword in article.WatsonKeyword)
                {
                    row.Add(watsonKeyword.Keyword);
                }

                foundKeyWordList.Add(row);
            }

            CSVHelper.ExporToCSV(pathDesktop, "watsonkeywords.csv", foundKeyWordList);

        }

        private static void SaveToDb(string name, List<string> keywordsFromArticle, List<string> watsonKeywords, List<string> concepts)
        {
            try
            {


                DiplProjectDb db = new DiplProjectDb();

                Article article = new Article() { ArticleName = name, Id = -1 };

                db.Article.Add(article);

                db.SaveChanges();

                if (keywordsFromArticle != null && keywordsFromArticle.Any())
                {
                    foreach (var keyword in keywordsFromArticle.Select(p => p.Trim()).Distinct().ToList())
                    {
                        db.ArticleKeyword.Add(new ArticleKeyword()
                        {
                            Id = -1,
                            ArticleId = article.Id,
                            Keyword = keyword.ToLower().Trim()
                        });
                    }
                }

                db.SaveChanges();
                if (watsonKeywords != null && watsonKeywords.Select(p => p.Trim()).Any())
                {
                    foreach (var watkeyword in watsonKeywords.Distinct().ToList())
                    {
                        db.WatsonKeyword.Add(new WatsonKeyword()
                        {
                            Id = -1,
                            ArticleId = article.Id,
                            Keyword = watkeyword.ToLower().Trim()
                        });
                    }
                }

                db.SaveChanges();
                if (concepts != null && concepts.Any())
                {
                    foreach (var concept in concepts.Select(p => p.Trim()).Distinct().ToList())
                    {
                        db.Concept.Add(new Concept() { Id = -1, ArticleId = article.Id, ConceptName = concept.ToLower().Trim() });
                    }
                }


                db.SaveChanges();
            }
            catch (Exception e)
            {
                using (StreamWriter writetext = new StreamWriter("exception.txt"))
                {

                    writetext.WriteLine(e.StackTrace.ToString());


                }

            }
        }
    }
}
