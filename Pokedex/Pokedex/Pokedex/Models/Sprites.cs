using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public partial class Sprites
    {
        [JsonProperty("front_default")]
        public Uri FrontDefault { get; set; }

    }
}
