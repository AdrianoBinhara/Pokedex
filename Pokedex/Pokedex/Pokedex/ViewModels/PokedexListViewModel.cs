using LiteDB;
using Newtonsoft.Json;
using Pokedex.Helpers;
using Pokedex.Mocks;
using Pokedex.Models;
using Pokedex.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
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
        private ObservableCollection<Pokemon> _fullListPokemon { get; set; }
        private string _colorBackgroundPoke { get; set; }
        private float _loadPokemon { get; set; }
        private bool _isLoading { get; set; }
        private string _image { get; set; }
        private string _name { get; set; }
        #endregion

        #region Properties
        LiteDatabase _dataBase;
        string identificador = "idImagem";

        public Pokemon SelectPoke { get; set; }
        public PokemonFromApi json;
        public Stream stream;
        HttpClient client;
        public INavigation navigation;
        public string Next;
        public string Previous;
        public string Url = "https://pokeapi.co/api/v2/pokemon";
        public ObservableCollection<Pokemon> FullListPokemon
        {
            get { return _fullListPokemon; }
            set
            {
                if (_fullListPokemon != value)
                {
                    _fullListPokemon = value;
                    OnPropertyChanged("FullListPokemon");
                }
            }
        }
        public float loadPokemon
        {
            get { return _loadPokemon; }
            set
            {
                if (_loadPokemon != value)
                {
                    _loadPokemon = value;
                    OnPropertyChanged();
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
        public Command FilterByElement { get; }

        #endregion
        public PokedexListViewModel(INavigation _navigation)
        {
            FullListPokemon = new ObservableCollection<Pokemon>();
            _dataBase = new LiteDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MeuBanco.db"));
            client = new HttpClient();
            navigation = _navigation;
            _ = GetPokemonList(Url);
            
            SelectedPokemon = new Command(async () => await OnSelectedPokemon());
        }

       


        #region Methods
        private async Task GetPokemonList(string url)
        {
            await GetPokemon(url);
        }

        private async Task OnSelectedPokemon()
        {
            await navigation.PushAsync(new PokemonDetail(SelectPoke));
        }
        public async Task<bool> GetPokemon(string url)
        {
            IsLoading = true;
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject<PokemonFromApi>(result);
            }
            AddToList();

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

                    try
                    {
                        if (!string.IsNullOrEmpty(jsonPoke.Sprites.FrontDefault.AbsoluteUri.ToString())) ;
                        poke.Image = jsonPoke.Sprites.FrontDefault.AbsoluteUri.ToString();
                        if (!string.IsNullOrEmpty(jsonPoke.Types.ElementAt(0).Type.Name)) ;
                        poke.Element = jsonPoke.Types.ElementAt(0).Type.Name;
                        if (jsonPoke.Stats != null)
                            poke.Stats = jsonPoke.Stats;

                        
                    }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Concluído", "Você ja pode utilizar sua pokedex completa!", "OK");
                        IsLoading = false;
                        return;
                    }

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

                    float longLoad = (float)FullListPokemon.Count;
                    loadPokemon = longLoad / 982;
                }
                FullListPokemon.Add(poke);
                if (_dataBase.FileStorage.Exists(identificador))
                {
                    stream = _dataBase.FileStorage.OpenRead(identificador);
                    poke.Image = ImageSource.FromStream(() => stream).ToString();
                }
                stream = GetImageStreamFromUrl(poke.Image);

                //Se existir a Stream
                if (stream != null)
                {
                    //Verfica se ja existe a imagem,se existir apaga
                    if (_dataBase.FileStorage.Exists(identificador))
                    {
                        _dataBase.FileStorage.Delete(identificador);
                    }
                    _dataBase.FileStorage.Upload(identificador, "Teste", stream);

                }

            }
            if (FullListPokemon.Count != 1050)
                await GetPokemonList(json.Next);
            else
                IsLoading = false;
        }
        private Stream GetImageStreamFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    Stream stream = new MemoryStream(imageBytes);
                    return stream;
                }
            }
            return null;
        }
        #endregion
    }
}
