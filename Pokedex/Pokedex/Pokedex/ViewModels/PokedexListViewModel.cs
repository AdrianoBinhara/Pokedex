using LiteDB;
using Newtonsoft.Json;
using Pokedex.Models;
using Pokedex.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Helpers;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Pokedex.Services;
using Xamarin.Essentials;
using Pokedex.Models.ModelLTB;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace Pokedex.ViewModels
{
    public class PokedexListViewModel : BaseViewModel
    {
        #region Private 
        private PokemonsService _pokemonService;
        private ObservableCollection<PokemonLTB> _fullListPokemonLTB { get; set; }
        private string _colorBackgroundPoke { get; set; }
        private bool _isLoading { get; set; }
        private string _pokeLoad { get; set; }
        #endregion

        #region Properties
        LiteDatabase _dataBase;
        public PokemonLTB SelectPoke { get; set; }
        public INavigation navigation;

        public string PokeLoad
        {
            get { return _pokeLoad; }
            set
            {
                if (_pokeLoad != value)
                {
                    _pokeLoad = value;
                    OnPropertyChanged("PokeLoad");
                }
            }
        }
        public ObservableCollection<PokemonLTB> FullListPokemonLTB
        {
            get { return _fullListPokemonLTB; }
            set
            {
                if (_fullListPokemonLTB != value)
                {
                    _fullListPokemonLTB = value;
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

        public Command SelectedPokemon { get; }
        public PokedexListViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            FullListPokemonLTB = new ObservableCollection<PokemonLTB>();
            _pokemonService = new PokemonsService();
            _dataBase = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "MeuBanco.db"));
            _ = GetPokemon();
            SelectedPokemon = new Command(async () => await OnSelectedPokemon());
        }

        #region Methods
        public async Task GetPokemon()
        {
            IsLoading = true;
            try
            {
                LiteCollection<PokemonLTB> pokemonsDB = (LiteCollection<PokemonLTB>)_dataBase.GetCollection<PokemonLTB>();
                if (pokemonsDB.Count() == 0)
                {
                    PokeLoad = "Recebendo Pokemons... Isto pode demorar um pouco.";
                    var pokemonsAPI = await _pokemonService.GetPokemonAsync();
                    foreach (var pokemon in pokemonsAPI)
                    {
                        PokemonLTB pokeLTB = new PokemonLTB
                        {
                            Id = pokemon.Id,
                            Name = pokemon.Name.ToUpper(),
                            ElementType = pokemon.Types.ElementAt(0).Type.Name,
                            Stats = pokemon.Stats,
                            BackgroundColor = pokemon.Types.ElementAt(0).Type.Name switch
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
                            }
                        };
                        pokemonsDB.Upsert(pokeLTB);
                        using (Stream stream = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri))
                        {
                            if (stream != null)
                            {
                                //Verfica se ja existe a imagem,se existir apaga
                                if (_dataBase.FileStorage.Exists(pokemon.Id.ToString()))
                                {
                                    _dataBase.FileStorage.Delete(pokemon.Id.ToString());
                                }
                                _dataBase.FileStorage.Upload(pokemon.Id.ToString(), pokemon.Name, stream);
                            }
                        }
                    }
                    pokemonsDB = (LiteCollection<PokemonLTB>)_dataBase.GetCollection<PokemonLTB>();
                }
                FullListPokemonLTB.Clear();
                foreach (var pokemon in pokemonsDB.FindAll())
                {
                    pokemon.Image = ImageSource.FromStream(() => _dataBase.FileStorage.FindById(pokemon.Id.ToString()).OpenRead());
                    FullListPokemonLTB.Add(pokemon);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
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
        private async Task OnSelectedPokemon()
        {
            await navigation.PushAsync(new PokemonDetail(SelectPoke));
        }
        #endregion
    }
}
