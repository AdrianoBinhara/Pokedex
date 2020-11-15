using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public partial class PokemonFromApi
    {
        [JsonProperty("count")]
        public long Count { get; set; }
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public Pokemon[] Results { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }
        [JsonProperty("stats")]
        public Stat[] Stats { get; set; }

        [JsonProperty("types")]
        public TypeElement[] Types { get; set; }

    }
}
