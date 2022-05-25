using Newtonsoft.Json;
using PrisonerZero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrisonerZero.Handlers
{
    internal class WeatherCommand
    {
        public static readonly IEnumerable<string> Commands = new[] { "w", "we", "weather", "п", "в", "погода" };

        const string Token = "6e1cbb8621c822b2412b7c2bbe5b8c09";
        const string Match = "^(?'city'[\\w \\-]+)";

        public static async Task<string> GetWeather(string payload)
        {
            var location = string.IsNullOrWhiteSpace(payload) ? "Рязань" : Regex.Match(payload, Match).Groups["city"].Value;

            var url = $"http://api.openweathermap.org/data/2.5/weather?q={location}&APPID={Token}&lang=ru&units=metric";
            try
            { 
                using var client = new WebClient();
                var result = await client.DownloadStringTaskAsync(url);
                var weather = JsonConvert.DeserializeObject<WeatherInfo>(result);
                return
                    $@"{weather.Name}:({weather.DateTime:HH:mm}) {weather.Main.Temperature}°C, {weather.Wind.Speed:0.#}м/с {weather.Main.Pressure:0.##}мм рт. ст., {weather.Main.Humidity:0}% влажн., {weather.Stations.First()?.Description}";
            }
            catch(Exception ex)
            {
                return $"{location}: {ex.Message}";
            }
        }
    }
}
