using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis;
using Google.Apis.Services;
using Google.Apis.CustomSearchAPI.v1;
using System.Net;

namespace PrisonerZero.Handlers
{
    internal class CalcCommand
    {
        public static readonly IEnumerable<string> Commands = new string[] { "calc", "c", "к", "с" };

        internal static async Task<string> GetResult(string payload)
        {
            if (!string.IsNullOrWhiteSpace(payload))
            {
                var url = $"https://api.mathjs.org/v4/?expr={payload}";
                using var client = new WebClient();
                var result = await client.DownloadStringTaskAsync(url);
                return result;
            }
            return "";

        }
    }
}