using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IBM.Watson.Project.Models;

namespace IBM.Watson.Project.Helpers.WatsonHelper
{
    public class WatsonHelperText : IWatsonHelper
    {
        public readonly string APIUrlStart = "https://gateway-a.watsonplatform.net/calls/text/TextGetRankedConcepts?text=";
        public readonly string APIUrlEnd = "&outputMode=json&extract=keywords,entities,concepts&sentiment=1&maxRetrieve=3&apikey=";
        private string apiKey { get; set; }

        public WatsonHelperText(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public AlchemyResponse GetResult(string input)
        {
            var urlEncode = HttpUtility.UrlEncode(input);
            var EndPointUrl = APIUrlStart + urlEncode + APIUrlEnd + apiKey;
            var response = RequestHelper.Get(EndPointUrl);

            return JSONHelper.Deserialize<AlchemyResponse>(response);
        }
    }
}
