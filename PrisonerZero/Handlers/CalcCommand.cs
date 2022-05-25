using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis;
using Google.Apis.Services;
using Google.Apis.CustomSearchAPI.v1;
using System.Net;
using System.Net.Http;

namespace PrisonerZero.Handlers
{
    internal class CalcCommand
    {
        public static readonly IEnumerable<string> Commands = new string[] { "calc", "c", "к", "с" };

        internal static async Task<string> GetResult(string payload)
        {
            if (!string.IsNullOrWhiteSpace(payload))
            {
                var url = $"https://api.mathjs.org/v4/?expr={WebUtility.UrlEncode(payload)}";
                using var client = new HttpClient();
                var result = await client.GetStringAsync(url);
                return result;
            }
            return "";

        }
    }
}