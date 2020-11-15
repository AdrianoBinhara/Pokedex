using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Pokedex.ViewModels
{
    public class PokemonDetailViewModel : BaseViewModel
    {
        private ObservableCollection<Stat> _listStats { get; set; }
        public ObservableCollection<Stat> ListStats
        {
            get { return _listStats; }
            set
            {
                if (_listStats != value)
                {
                    _listStats = value;
                    OnPropertyChanged("ListStats");
                }
            }
        }
        public Pokemon Pokemon { get; set; }
        public PokemonDetailViewModel(Pokemon pokemon)
        {
            Pokemon = pokemon;
            ListStats = new ObservableCollection<Stat>();
            GetStats();
        }

        private void GetStats()
        { 
            foreach (var st in Pokemon.Stats)
            {
                ListStats.Add(st);
            }

        }
    }
}
