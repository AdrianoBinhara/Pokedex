using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pokedex.Models.ModelLTB
{
    public class PokemonLTB
    {
        [BsonId]
        public long Id { get; set; }
        public string Name { get; set; }
        public SpritesLDB Sprites { get; set; }
        public byte[] ImageByte { get; set; }
        public string ElementType { get; set; }
        public string BackgroundColor { get; set; }
        public Stat[] Stats { get; set; }
        [BsonIgnore]
        public ImageSource Image { get; set; }

        
    }

    public class SpritesLDB
    {
        public Uri FrontDefautl { get; set; }
    }
}
