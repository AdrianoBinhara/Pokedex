using Pokedex.Models;
using Pokedex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetail : ContentPage
    {
        public PokemonDetail(Pokemon poke)
        {
            InitializeComponent();
            BindingContext = new PokemonDetailViewModel(poke);
        }
    }
}