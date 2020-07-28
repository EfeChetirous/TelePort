using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teleport.Model.Models
{
    public class AirportInfoModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city_iata")]
        public string CityIata { get; set; }

        [JsonProperty("iata")]
        public string Iata { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("timezone_region_name")]
        public string TimezoneRegionName { get; set; }

        [JsonProperty("country_iata")]
        public string CountryIata { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("hubs")]
        public int Hubs { get; set; }
    }
    public class Location
    {

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

    }
}
