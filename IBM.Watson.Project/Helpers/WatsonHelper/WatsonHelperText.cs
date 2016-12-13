using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public AlchemyResponse GetResult1(string input)
        {
            var urlEncode = HttpUtility.UrlEncode(input);
            var EndPointUrl = APIUrlStart + urlEncode + APIUrlEnd + apiKey;
            var response = RequestHelper.Get(EndPointUrl);

            return JSONHelper.Deserialize<AlchemyResponse>(response);
        }

        public AlchemyResponse GetResult(string input)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("outputMode", "json"),
                new KeyValuePair<string, string>("extract", "entities,keywords,authors,concepts,taxonomy"),
                new KeyValuePair<string, string>("url", "https://www.ibm.com/us-en/"),
                new KeyValuePair<string, string>("text", input)
            };

            var content = new FormUrlEncodedContent(pairs);

            var client = new HttpClient { BaseAddress = new Uri("https://gateway-a.watsonplatform.net") };

            // call sync
            var response = client.PostAsync("/calls/text/TextGetCombinedData?apikey=" + apiKey, content).Result;

            if (response.IsSuccessStatusCode)
            {

                string resultContent = response.Content.ReadAsStringAsync().Result;

                return JSONHelper.Deserialize<AlchemyResponse>(resultContent);
            }

            return null;
        }
    }
}
