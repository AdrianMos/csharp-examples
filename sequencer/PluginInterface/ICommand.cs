using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PluginInterface
{
    /// <summary>
    /// The interface shared by all Plugins and the Sequencer
    /// </summary>
    public interface ICommand
    {
        bool Init(object[] args);
        void Execute(object[] args);
        bool DeInit();
    }
}
