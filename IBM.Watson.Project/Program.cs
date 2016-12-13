using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
            IPDFParser pdfParser = new ITextSharpPDFParser();

            var text = pdfParser.ExtractTextFromPdf("test.pdf");

            IWatsonHelper helper = new WatsonHelperText(APIKey);

            var result = helper.GetResult(text);
            Console.WriteLine(JSONHelper.Serialize(result));

            Console.WriteLine();
            Console.WriteLine("-- ------------------------------- --");
            Console.WriteLine();

            //IWatsonHelper helpUrl = new WatsonHelperUrl(APIKey);

            //var resultUrl= helpUrl.GetResult("http://www.baynews9.com/alerts.html");

            //Console.WriteLine(JSONHelper.Serialize(result));

            Console.ReadLine();
        }
    }
}
