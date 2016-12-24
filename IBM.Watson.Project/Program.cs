using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using IBM.Watson.Project.Helpers;
using IBM.Watson.Project.Helpers.PDFParser;
using IBM.Watson.Project.Helpers.WatsonHelper;

namespace IBM.Watson.Project
{
    class Program
    {
        private static readonly string APIKey = "ea4cb11ced2395985e21914549b176c68111faac";
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.ASCII;

            IPDFParser pdfParser = new ITextSharpPDFParser();

            var filename = "ComSIS_526-1410.pdf";

            var text = pdfParser.ExtractTextFromPdf(filename);

            var name = filename.Replace(".pdf", "");

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
                return;
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



            //IWatsonHelper helpUrl = new WatsonHelperUrl(APIKey);

            //var resultUrl= helpUrl.GetResult("http://www.baynews9.com/alerts.html");

            //Console.WriteLine(JSONHelper.Serialize(result));

            Console.ReadLine();
        }
    }
}
