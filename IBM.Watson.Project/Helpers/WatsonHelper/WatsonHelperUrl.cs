using IBM.Watson.Project.Models;

namespace IBM.Watson.Project.Helpers.WatsonHelper
{

    public class WatsonHelperUrl: IWatsonHelper
    {
        public readonly string APIUrlStart = "https://gateway-a.watsonplatform.net/calls/url/URLGetCombinedData?url=";
        public readonly string APIUrlEnd = "&outputMode=json&extract=keywords,entities,concepts&sentiment=1&maxRetrieve=3&apikey=";
        private string apiKey { get; set; }

        public WatsonHelperUrl(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public AlchemyResponse GetResult(string input)
        {
            var EndPointUrl = APIUrlStart + input + APIUrlEnd + apiKey;
            var response = RequestHelper.Get(EndPointUrl);

            return JSONHelper.Deserialize<AlchemyResponse>(response);
        }
    }
}
