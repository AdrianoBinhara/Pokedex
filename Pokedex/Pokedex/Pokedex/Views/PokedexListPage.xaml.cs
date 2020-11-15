using Pokedex.Mocks;
using Pokedex.Models;
using Pokedex.Models.ModelLTB;
using Pokedex.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokedexListPage : ContentPage
    {
        public PokedexListViewModel pokemonViewModel { get; set; }
        public PokedexListPage()
        {
            InitializeComponent();
            GetMockButton();
            pokemonViewModel = new PokedexListViewModel(Navigation);
            BindingContext = pokemonViewModel;
        }

        private void GetMockButton()
        {
            MockFilterButton mockbt = new MockFilterButton();
            CollectionButton.ItemsSource = mockbt.Get();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {  
            if (string.IsNullOrEmpty(e.NewTextValue))
                PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemonLTB;
            
            else
            {
                PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemonLTB;
                IEnumerable<PokemonLTB> enumerable = pokemonViewModel.FullListPokemonLTB.Where(p => p.Name.Contains(e.NewTextValue.ToUpper()));
                var observableFiltered = new ObservableCollection<PokemonLTB>(enumerable.ToList());
                PokemonCollectionView.ItemsSource= observableFiltered;
            }
        }

        private void PokemonCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string current = (e.CurrentSelection.FirstOrDefault() as FilterButton)?.Icon;
            string url = current switch
            {
                "normal.png" => url = "normal",
                "fight.png" => url = "fighting",
                "flying.png" => url = "flying",
                "poison.png" => url = "poison",
                "ground.png" => url = "ground",
                "rock.png" => url = "rock",
                "bug.png" => url = "bug",
                "ghost.png" => url = "ghost",
                "steel.png" => url = "steel",
                "fire.png" => url = "fire",
                "water.png" => url = "water",
                "grass.png" => url = "grass",
                "eletric.png" => url = "electric",
                "psychic.png" => url = "psychic",
                "ice.png" => url = "ice",
                "dragon.png" => url = "dragon",
                "dark.png" => url = "dark",
                "fairy.png" => url = "fairy",
                "shadow.png" => url = "shadow",
                _ => url = ""
            };
            PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemonLTB;
            IEnumerable<PokemonLTB> eumerable = pokemonViewModel.FullListPokemonLTB.Where(p => p.ElementType.Contains(url));
            var observableFiltered = new ObservableCollection<PokemonLTB>(eumerable.ToList());
            PokemonCollectionView.ItemsSource= observableFiltered;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PokemonCollectionView.ItemsSource = null;
            PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemonLTB;
        }
    }
}