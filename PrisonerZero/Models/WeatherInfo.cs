using Newtonsoft.Json;
using System;

namespace PrisonerZero.Models
{
    public class Coord
    {
        [JsonProperty("lat")] public decimal Latitude { get; set;}
        [JsonProperty("lon")] public decimal Longitude { get; set;}
    }

    public class Station
    {
        [JsonProperty("id")] public int Id { get; set;}
        [JsonProperty("main")] public string Main { get; set;}
        [JsonProperty("description")] public string Description { get; set;}
        [JsonProperty("icon")] public string Icon { get; set;}
    }

    public class Wind
    {
        [JsonProperty("speed")] public decimal Speed { get; set;}
        [JsonProperty("deg")] public decimal Deg { get; set;}
        [JsonProperty("gust")] public decimal Gust { get; set;}
    }

    public class MainInfo
    {
        [JsonProperty("temp")] public decimal Temperature { get; set;}
        [JsonProperty("feels_like")] public decimal FeelsLike { get; set;}
        [JsonProperty("temp_min")] public decimal TempMin { get; set;}
        [JsonProperty("temp_max")] public decimal TempMax { get; set;}
        [JsonProperty("pressure")] public decimal PressureHpa { get; set;}
        [JsonProperty("humidity")] public decimal Humidity { get; set;}
        [JsonProperty("sea_level")] public decimal SeaLevel { get; set;}
        [JsonProperty("grnd_level")] public decimal GroundLevel { get; set;}

        [JsonIgnore] public decimal Pressure => PressureHpa / 10.0m * 7.50063m;
    }

    internal class SysInfo
    {
        [JsonProperty("type")] public int Type { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
    }

    internal class WeatherInfo
    {
        [JsonProperty("coord")] public Coord Coord = null;
        [JsonProperty("weather")] public Station[] Stations = null;
        [JsonProperty("wind")] public Wind Wind = null;
        [JsonProperty("main")] public MainInfo Main = null;
        [JsonProperty("sys")] public SysInfo Sys = null;
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("dt")] public long EpochTimeSeconds { get; set; }

        [JsonIgnore] public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(EpochTimeSeconds).ToLocalTime().DateTime;
    }
}
