using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public partial class Stat
    {
        [JsonProperty("base_stat")]
        public float BaseStat { get; set; }

        [JsonProperty("stat")]
        public Species StatStat { get; set; }
        public long LoadStat { get; set; }
    }

    public class Species
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}


