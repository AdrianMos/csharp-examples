using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerApp
{
    interface IPluginsManager
    {
        bool ArePluginsLoaded(List<string> commandNames);
        void LoadPlugins(List<string> commandNames);
        PluginInterface.ICommand GetCommand(string commandName);
    }
}
