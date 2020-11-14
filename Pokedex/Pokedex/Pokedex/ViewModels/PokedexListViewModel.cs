using Newtonsoft.Json;
using Pokedex.Models;
using Pokedex.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pokedex.ViewModels
{
    public class PokedexListViewModel : BaseViewModel
    {
        #region Private 
        private bool _isLoading { get; set; }
        private string _image { get; set; }
        private string _name { get; set; }
        private ObservableCollection<Pokemon> _ListPokemon { get; set; }
        #endregion

        #region Properties
        HttpClient client;
        private string _colorBackgroundPoke { get; set; }
        public PokemonFromApi json;
        public Pokemon SelectPoke { get; set; }
        public INavigation navigation;
        public string Next;
        public string Previous;
        public string Url = "https://pokeapi.co/api/v2/pokemon";

        public ObservableCollection<Pokemon> ListPokemon
        {
            get { return _ListPokemon; }
            set
            {
                if (_ListPokemon !=value)
                {
                    _ListPokemon = value;
                    OnPropertyChanged("ListPokemon");
                }
            }
        }
       
        public string ColorBackgroundPoke 
        {
            get { return _colorBackgroundPoke; }
            set
            {
                if (_colorBackgroundPoke != value)
                {
                    _colorBackgroundPoke = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }
       
        #endregion
        #region Commands
        public Command NextCommand { get; }
        public Command SelectedPokemon { get; }

        #endregion
        public PokedexListViewModel(INavigation _navigation)
        {
            ListPokemon = new ObservableCollection<Pokemon>();
            
            client = new HttpClient();
            navigation = _navigation;
            IsLoading = true;
           _= GetPokemonList(Url);
           // GetPokemonByType(Url);
            NextCommand = new Command(async () => await OnNextCommand());
            SelectedPokemon = new Command(async () => await OnSelectedPokemon());
        }

        private async Task GetPokemonList(string url)
        {
            await GetPokemon(url);
            AddToList();
        }
        private async Task OnNextCommand()
        {
            await GetPokemonList(Next);
        }

        private async Task GetPokemonByType(string url)
        {
           await GetPokemon(url + "?offset=0limit=1050");
        }

        private async Task OnSelectedPokemon( )
        {
            await navigation.PushAsync(new PokemonDetail(SelectPoke));
        }
        #region Methods
        public async Task<bool> GetPokemon(string url)
        {
            IsLoading = true;

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject<PokemonFromApi>(result);
                Next = json.Next;
            }
            IsLoading = false;
            return true;
        }

        private async void AddToList()
        {
            IsLoading = true;
            foreach (var item in json.Results)
            {

                var poke = new Pokemon
                {
                    Name = item.Name,
                    Url = item.Url
                };
                var r = await client.GetAsync(item.Url);
                if (r.IsSuccessStatusCode)
                {
                    var resultPoke = await r.Content.ReadAsStringAsync();
                    var jsonPoke = JsonConvert.DeserializeObject<PokemonFromApi>(resultPoke);

                    poke.Image = jsonPoke.Sprites.FrontDefault.AbsoluteUri.ToString();
                    poke.Element = jsonPoke.Types.ElementAt(0).Type.Name;
                    poke.Stats = jsonPoke.Stats;
                   
                    ColorBackgroundPoke = poke.Element switch
                    {
                        "normal" => ColorBackgroundPoke = "#394F68",
                        "fighting" => ColorBackgroundPoke = "#D14461",
                        "flying" => ColorBackgroundPoke = "#E4ECFB",
                        "poison" => ColorBackgroundPoke = "#B667CD",
                        "ground" => ColorBackgroundPoke = "#D87C52",
                        "rock" => ColorBackgroundPoke = "#C9BB8D",
                        "bug" => ColorBackgroundPoke = "#93BB3A",
                        "ghost" => ColorBackgroundPoke = "#606FBA",
                        "steel" => ColorBackgroundPoke = "#5995A2",
                        "fire" => ColorBackgroundPoke = "#F9A555",
                        "water" => ColorBackgroundPoke = "#579EDD",
                        "grass" => ColorBackgroundPoke = "#DCF3DA",
                        "electric" => ColorBackgroundPoke = "#F1D85A",
                        "psychic" => ColorBackgroundPoke = "#F88684",
                        "ice" => ColorBackgroundPoke = "#79D0C1",
                        "dragon" => ColorBackgroundPoke = "#176CC5",
                        "dark" => ColorBackgroundPoke = "#595761",
                        "fairy" => ColorBackgroundPoke = "#ED93E4",
                        "shadow" => ColorBackgroundPoke = "#EBEBEB",
                        _ => ColorBackgroundPoke = "#EBEBEB"
                    };
                    poke.BackGroundcolor = ColorBackgroundPoke;
                }
                ListPokemon.Add(poke);
            }
            IsLoading = false;
        }
        #endregion
    }
}
