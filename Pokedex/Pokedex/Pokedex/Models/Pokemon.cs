using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pokedex.Models
{
    public class Pokemon
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }
        [JsonProperty("typeelement")]
        public string Element { get; set; }
        [JsonProperty("stats")]
        public Stat[] Stats { get; set; }
        public string BackGroundcolor { get; set; }


    }
}
