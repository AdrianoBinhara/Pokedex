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
        private bool _isVisible { get; set; }
        private string _image { get; set; }
        private string _name { get; set; }
        private List<Pokemon> _listPoke;
        #endregion

        #region Properties
        HttpClient client = new HttpClient();
        public PokemonFromApi json;
        readonly List<Pokemon> ListPokemon = new List<Pokemon>();
        public Pokemon SelectPoke { get; set; }
        public INavigation navigation;
        public string Next;
        public string Previous;
        public string Url = "https://pokeapi.co/api/v2/pokemon";

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<Pokemon> listPoke
        {
            get { return _listPoke; }
            set
            {
                SetProperty(ref _listPoke, value);
                OnPropertyChanged("ListPokemon");
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
        public Command PreviousCommand { get; }
        public Command SelectedPokemon { get; }

        #endregion
        public PokedexListViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            IsLoading = true;
           _= GetPokemonList(Url);
           // GetPokemonByType(Url);
            NextCommand = new Command(async () => await OnNextCommand());
            SelectedPokemon = new Command(async () => await OnSelectedPokemon());
        }

        private async Task GetPokemonList(string url)
        {
            IsLoading = true;
            await GetPokemon(url);
            AddToList();
            IsLoading = false;
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
           
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject<PokemonFromApi>(result);
                Next = json.Next;
            }
            
            return true;
        }

        private async void AddToList()
        {
            foreach (var item in json.Results)
            {

                var poke = new Pokemon();
                poke.Name = item.Name;
                poke.Url = item.Url;
                var r = await client.GetAsync(item.Url);
                if (r.IsSuccessStatusCode)
                {
                    var resultPoke = await r.Content.ReadAsStringAsync();
                    var jsonPoke = JsonConvert.DeserializeObject<PokemonFromApi>(resultPoke);

                    poke.Image = jsonPoke.Sprites.FrontDefault.AbsoluteUri.ToString();
                }
                ListPokemon.Add(poke);
            }

            //Utilizei apenas para recarregar a lista, mas pode ser melhorado
            listPoke = null;
            listPoke = ListPokemon;
            foreach (var i in listPoke)
            {
                Image = i.Image;
                Name = i.Name;
            }
        }

        private async Task OnNextCommand()
        {
            await GetPokemonList(Next);
        }
        #endregion
    }
}
