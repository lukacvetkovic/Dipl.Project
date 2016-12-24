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
                //new KeyValuePair<string, string>("extract", "authors,concepts,dates,doc-emotion,entities,feeds,keywords,pub-date,relations,typed-rels,doc-sentiment,taxonomy,title"),
                new KeyValuePair<string, string>("extract", "authors,concepts,entities,keywords,title"),
                new KeyValuePair<string, string>("url", "https://www.ibm.com/us-en/"),
                new KeyValuePair<string, string>("text", TruncateLongString(input,65349))
            };
            //int limit = 2000;

            //StringContent content = new StringContent(pairs.Aggregate(new StringBuilder(), (sb, nxt) => {

            //    StringBuilder sbInternal = new StringBuilder();
            //    int loops = nxt.Value.Length / limit;

            //    for (int i = 0; i <= loops; i++)
            //    {
            //        if (i < loops)
            //        {
            //            sbInternal.Append(Uri.EscapeDataString(nxt.Value.Substring(limit * i, limit)));
            //        }
            //        else
            //        {
            //            sbInternal.Append(Uri.EscapeDataString(nxt.Value.Substring(limit * i)));
            //        }
            //    }

            //    return sb.Append(nxt.Key + "=" + sbInternal.ToString());
            //}).ToString());

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

        private string TruncateLongString(string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}
