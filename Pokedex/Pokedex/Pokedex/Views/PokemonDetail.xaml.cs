using Pokedex.Models.ModelLTB;
using Pokedex.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetail : ContentPage
    {
        public PokemonDetail(PokemonLTB poke)
        {
            InitializeComponent();
            BindingContext = new PokemonDetailViewModel(poke);
        }
    }
}