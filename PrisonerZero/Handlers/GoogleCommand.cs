using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis;
using Google.Apis.Services;
using Google.Apis.CustomSearchAPI.v1;

namespace PrisonerZero.Handlers
{
    internal class GoogleCommand
    {
        public static readonly IEnumerable<string> Commands = new string[] { "google", "g" };

        internal static async Task<string> GetResult(string payload)
        {
            // return "";
            var service = new CustomSearchAPIService(new BaseClientService.Initializer
                {ApiKey = Configuration.GoogleApiKey});
            var request = service.Cse.List();
            request.Cx = Configuration.GoogleSearchId;
            request.ExactTerms = payload;
            request.Q = payload;
            var result = await request.ExecuteAsync();
            return "";
        }
    }
}