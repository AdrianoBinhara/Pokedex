using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.ViewModels
{
    public class PokemonDetailViewModel
    {
        public Pokemon Pokemon { get; set; }
        public PokemonDetailViewModel(Pokemon pokemon)
        {
            Pokemon = pokemon;

        }
    }
}
