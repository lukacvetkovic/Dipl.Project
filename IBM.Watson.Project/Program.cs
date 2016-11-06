using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IBM.Watson.Project.Helpers;
using IBM.Watson.Project.Helpers.WatsonHelper;

namespace IBM.Watson.Project
{
    class Program
    {
        private static readonly string APIKey = "ea4cb11ced2395985e21914549b176c68111faac";
        static void Main(string[] args)
        {
            IWatsonHelper helper = new WatsonHelperText(APIKey);

            var result = helper.GetResult("A Secret Service spokesperson said in a statement there was a commotion in the crowd and an \"unidentified individual\" shouted \"gun,\" though no weapon was found after a \"thorough search.\"");

            Console.WriteLine(JSONHelper.Serialize(result));

            Console.ReadLine();
        }
    }
}
