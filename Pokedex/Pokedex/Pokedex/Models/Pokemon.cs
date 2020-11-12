using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pokedex.Models
{
    public class Pokemon
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
        [JsonProperty("urlimages")]
        public Sprites UrlImages { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }


    }
}
