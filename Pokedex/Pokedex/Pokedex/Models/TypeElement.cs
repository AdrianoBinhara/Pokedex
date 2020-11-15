using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public partial class TypeElement
    {
        [JsonProperty("type")]
        public Species Type { get; set; }
    }
}
