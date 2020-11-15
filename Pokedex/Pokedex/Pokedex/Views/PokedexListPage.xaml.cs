using Pokedex.Mocks;
using Pokedex.Models;
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
        private ObservableCollection<Pokemon> _filteredListPokemon { get; set; }
        public FilterButton filterButton { get; set; }
        public PokedexListViewModel pokemonViewModel { get; set; }
        public ObservableCollection<Pokemon> FilteredList
        {
            get { return _filteredListPokemon; }
            set
            {
                if (_filteredListPokemon != value)
                {
                    _filteredListPokemon = value;
                    OnPropertyChanged("FilteredList");
                }
            }
        }
        public PokedexListPage()
        {
            InitializeComponent();
            GetMockButton();
            FilteredList = new ObservableCollection<Pokemon>();
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
                PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemon;
            
            else
            {
                PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemon;
                IEnumerable<Pokemon> enumerable = pokemonViewModel.FullListPokemon.Where(p => p.Name.Contains(e.NewTextValue));
                var observableFiltered = new ObservableCollection<Pokemon>(enumerable.ToList());
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
            PokemonCollectionView.ItemsSource = null;
            PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemon;
            IEnumerable<Pokemon> eumerable = pokemonViewModel.FullListPokemon.Where(p => p.Element.Contains(url));
            var observableFiltered = new ObservableCollection<Pokemon>(eumerable.ToList());
            PokemonCollectionView.ItemsSource= observableFiltered;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PokemonCollectionView.ItemsSource = null;
            PokemonCollectionView.ItemsSource = pokemonViewModel.FullListPokemon;
        }
    }
}