using Pokedex.Models;
using Pokedex.Models.ModelLTB;
using System.Collections.ObjectModel;

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
        public PokemonLTB Pokemon { get; set; }
        public PokemonDetailViewModel(PokemonLTB pokemon)
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
