using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerApp
{
    /// <summary>
    /// Interface for passing application paths.
    /// </summary>
    interface IPaths
    {
        string PluginsFolder { get; }
        string ScriptsFolder { get; }
    }
}
