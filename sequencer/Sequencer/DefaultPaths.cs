using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SequencerApp
{
    /// <summary>
    /// The default paths are stored in this class (script folder, plugins folder)
    /// </summary>
    class DefaultPaths : IPaths
    {
        public string PluginsFolder
        {
            get
            {
                return Directory.GetCurrentDirectory() + "\\..\\..\\..\\Plugins\\";
            }
        }

        public string ScriptsFolder
        {
            get
            {
                return Directory.GetCurrentDirectory() + "\\..\\..\\..\\sequences\\";
            }
        }
    }
}
