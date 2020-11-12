using Newtonsoft.Json;
using Pokedex.Models;
using Pokedex.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokedexListPage : ContentPage
    {
        public string Next;
        public string Previous;
        public string Url;
        public PokedexListPage()
        {
            InitializeComponent();
            BindingContext = new PokedexListViewModel();
        }
        protected override async void OnAppearing()
        {
            Url = "https://pokeapi.co/api/v2/pokemon";
            await GetPokemon(Url);
            base.OnAppearing();
        }

        public async Task GetPokemon(string url)
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
                    btnPrevious.IsVisible = false;
                }
                else
                {
                    btnPrevious.IsVisible = true;
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
                        var jsonPoke = JsonConvert.DeserializeObject<PokemonStatsFromApi>(resultPoke);

                        poke.Image = jsonPoke.Sprites.FrontDefault.AbsoluteUri.ToString();
                    }
                    ListPokemon.Add(poke);
                }
                listPoke.ItemsSource = ListPokemon;
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await GetPokemon(Previous);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await GetPokemon(Next);
        }
    }
}