using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public partial class PokemonStatsFromApi
    {
        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }
    }
}
