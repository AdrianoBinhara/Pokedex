using Newtonsoft.Json;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private bool _isVisible {get;set;}
        private string _image { get; set; }
        private string _name { get; set; }
        private List<Pokemon> _listPoke { get; set; }
        #endregion

        #region Properties
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
                if (_listPoke != value)
                {
                    _listPoke = value;
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
        public string Next;
        public string Previous;
        public string Url = "https://pokeapi.co/api/v2/pokemon";
        #endregion
        #region Commands
        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        #endregion
        public PokedexListViewModel()
        {
                _ = GetPokemon(Url);
            NextCommand = new Command(async () => await OnNextCommand());
            PreviousCommand = new Command(async () => await OnPreviousCommand());
        }
        #region Methods
        public async Task<bool> GetPokemon(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<PokemonFromApi>(result);

                Next = json.Next;
                Previous = json.Previous;
                if (string.IsNullOrEmpty(Previous))
                {
                    IsVisible = false;
                }
                else
                {
                    IsVisible = true;
                }

                List<Pokemon> ListPokemon = new List<Pokemon>();
                foreach (var item in json.Results)
                {
                    Pokemon poke = new Pokemon();
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
                listPoke = ListPokemon;
                foreach (var i in listPoke)
                {
                    Image = i.Image;
                    Name = i.Name;
                }
            }
            return true;
        }
        private async Task OnPreviousCommand()
        {
            await GetPokemon(Previous);
        }
        private async Task OnNextCommand()
        {
            await GetPokemon(Next);
        }
        #endregion
    }
}
