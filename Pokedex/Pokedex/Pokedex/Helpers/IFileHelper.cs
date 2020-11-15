using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Helpers
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
