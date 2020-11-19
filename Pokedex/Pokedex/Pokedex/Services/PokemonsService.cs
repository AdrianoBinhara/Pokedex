    using Pokedex.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Helpers;
using HttpExtension;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace Pokedex.Services
{
    public class PokemonsService
    {
        public async Task<List<PokemonFromApi>> GetPokemonAsync()
        {
            List<PokemonFromApi> pokemons = new List<PokemonFromApi>();
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string api = "https://pokeapi.co/api/v2/pokemon/";
                for (int i = 1; i < 1050; i++)
                {
                    var response = await httpClient.GetAsync<PokemonFromApi>($"{api}{i}");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        pokemons.Add(response.Value);
                    }
                    else
                    {
                        Debug.WriteLine(response.Error.Message);
                    }
                }
    }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return pokemons;
        }
    }
}
