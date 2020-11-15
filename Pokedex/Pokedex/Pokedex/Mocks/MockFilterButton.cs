using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Pokedex.Mocks
{
    public class MockFilterButton
    {
        public ObservableCollection<FilterButton> Button { get; set; }
        public ObservableCollection<FilterButton> Get()
        {
            Button = new ObservableCollection<FilterButton>();
            Button.Add(new FilterButton
            {
                BackgroundColor = "#93BB3A",
                Icon = "bug.png",
                TypeElement = 0
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#595761",
                Icon = "dark.png",
                TypeElement = 1
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#176CC5",
                Icon = "dragon.png",
                TypeElement = 2
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#ED93E4",
                Icon = "fairy.png",
                TypeElement = 3
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#D14461",
                Icon = "fight.png",
                TypeElement = 4
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#A2BCEA",
                Icon = "flying.png",
                TypeElement = 5
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#606FBA",
                Icon = "ghost.png",
                TypeElement = 6
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#DCF3DA",
                Icon = "grass.png",
                TypeElement = 7
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#79D0C1",
                Icon = "ice.png",
                TypeElement = 8
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#A0A29F",
                Icon = "normal.png",
                TypeElement = 9
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#B667CD",
                Icon = "poison.png",
                TypeElement = 10
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#F88684",
                Icon = "psychic.png",
                TypeElement = 11
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#C9BB8D",
                Icon = "rock.png",
                TypeElement = 12
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#5995A2",
                Icon = "steel.png",
                TypeElement = 13
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#579EDD",
                Icon = "water.png",
                TypeElement = 14
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#D87C52",
                Icon = "ground.png",
                TypeElement = 15
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#F1D85A",
                Icon = "eletric.png",
                TypeElement = 16
            });
            Button.Add(new FilterButton
            {
                BackgroundColor = "#F9A555",
                Icon = "fire.png",
                TypeElement = 17    
            });
            return Button;
        }
    }
    public class FilterButton
    {
        public string BackgroundColor { get; set; }
        public string Icon { get; set; }
        public int TypeElement { get; set; }
    }
}
