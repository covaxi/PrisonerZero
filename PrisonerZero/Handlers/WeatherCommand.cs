using Newtonsoft.Json;
using PrisonerZero.Consts;
using PrisonerZero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrisonerZero.Handlers
{
    internal partial class WeatherCommand
    {
        public static readonly IEnumerable<string> Commands = new[] { "w", "we", "weather", "п", "в", "погода" };

        const string Token = "6e1cbb8621c822b2412b7c2bbe5b8c09";
        const string CityMatch = "^(?'city'[\\w \\-]+)";

        public static async Task<string> GetWeather(string payload)
        {
            var location = string.IsNullOrWhiteSpace(payload) ? "Ashdod" : CityRegex().Match(payload).Groups["city"].Value;

            var url = $"http://api.openweathermap.org/data/2.5/weather?q={location}&APPID={Token}&lang=ru&units=metric";
            try
            { 
                using var client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var weather = JsonConvert.DeserializeObject<WeatherInfo>(result);
                return
                    $@"{weather.Name}:({weather.DateTime:HH:mm}) {weather.Main.Temperature}°C, {weather.Wind.Speed:0.#}м/с (порывы до {weather.Wind.Gust:0.#} м/с) {Direction.GetArrow(weather.Wind.Deg)}({weather.Wind.Deg:0}°) {weather.Main.Pressure:0.##}мм рт. ст., {weather.Main.Humidity:0}% влажн., {weather.Stations.First()?.Description}";
            }
            catch(Exception ex)
            {
                return $"{location}: {ex.Message}";
            }
        }

        [GeneratedRegex(CityMatch, RegexOptions.CultureInvariant)]
        private static partial Regex CityRegex();
    }
}
