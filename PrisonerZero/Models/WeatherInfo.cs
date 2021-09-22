using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonerZero.Models
{
    public class Coord
    {
        [Newtonsoft.Json.JsonProperty("lat")]
        public decimal Latitude { get;set;}
        [Newtonsoft.Json.JsonProperty("lon")]
        public decimal Longitude {  get;set;}
    }
    public class Station
    {
        [JsonProperty("id")]
        public int Id { get;set;}
        [JsonProperty("main")]
        public string Main { get;set;}
        [JsonProperty("description")]
        public string Description { get;set;}
        [JsonProperty("icon")]
        public string Icon { get;set;}
    }
    public class Wind
    {
        [JsonProperty("speed")]
        public decimal Speed { get;set;}
        [JsonProperty("deg")]
        public decimal Deg { get;set;}
        [JsonProperty("gust")]
        public decimal Gust { get;set;}
    }
    public class MainInfo
    {
        [JsonProperty("temp")]
        public decimal Temperature { get;set;}
        [JsonProperty("feels_like")]
        public decimal FeelsLike { get;set;}
        [JsonProperty("temp_min")]
        public decimal TempMin { get;set;}
        [JsonProperty("temp_max")]
        public decimal TempMax { get;set;}
        [JsonProperty("pressure")]
        public decimal PressureHpa { get;set;}
        [JsonIgnore]
        public decimal Pressure => PressureHpa / 10.0m * 7.50063m;
        [JsonProperty("humidity")]
        public decimal Humidity { get;set;}
        [JsonProperty("sea_level")]
        public decimal SeaLevel { get;set;}
        [JsonProperty("grnd_level")]
        public decimal GroundLevel { get;set;}
    }

    internal class SysInfo
    {
        [JsonProperty("type")] public int Type { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
    }

    internal class WeatherInfo
    {
        [JsonProperty("coord")] public Coord Coord;
        [JsonProperty("weather")] public Station[] Stations;
        [JsonProperty("wind")] public Wind Wind;
        [JsonProperty("main")] public MainInfo Main;
        [JsonProperty("sys")] public SysInfo Sys;
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("dt")] public long EpochTimeSeconds { get; set; }
        [JsonIgnore] public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(EpochTimeSeconds).ToLocalTime().DateTime;
    }
}
