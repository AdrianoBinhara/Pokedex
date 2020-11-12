using Pokedex.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokedexListPage : ContentPage
    {
        
        public PokedexListPage()
        {
            InitializeComponent();
            BindingContext = new PokedexListViewModel();
        }
    }
}