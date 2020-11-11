﻿using Pokedex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokedexListPage : ContentPage
    {
        public string Url;
        public PokedexListPage()
        {
            InitializeComponent();
            BindingContext = new PokedexListViewModel();
        }
        protected override async void OnAppearing()
        {
            Url = "https://pokeapi.co/api/v2/pokemon";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Url);
            if (response.IsSuccessStatusCode)
            {

            }

            base.OnAppearing();
        }
    }
}