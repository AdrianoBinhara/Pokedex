using Pokedex.Models;
using Pokedex.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokedexListPage : ContentPage
    {
        public PokedexListPage()
        {
            InitializeComponent();
            BindingContext = new PokedexListViewModel(Navigation);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as PokedexListViewModel;
           
            if (string.IsNullOrEmpty(e.NewTextValue))
                PokemonCollectionView.ItemsSource = _container.listPoke;
            
            else
            {
                IEnumerable<Pokemon> enumerable = _container.listPoke.Where(p => p.Name.Contains(e.NewTextValue));
                enumerable = new List<Pokemon>(enumerable);
                PokemonCollectionView.ItemsSource = enumerable;
            }
        }
    }
}