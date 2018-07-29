using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SequencerApp
{
    class  PluginsManager : IPluginsManager
    {
        /// <summary>
        /// A paths object is used for path management.
        /// </summary>
        private IPaths paths;

        /// <summary>
        /// Mapping between command names and the loaded plugins.
        /// </summary>
        private Dictionary<string, PluginInterface.ICommand> dispatcher;

        public PluginsManager(IPaths paths)
        {
            this.paths = paths;
            this.dispatcher = new Dictionary<string, PluginInterface.ICommand>();
        }

        /// <summary>
        /// Loads commands from the .dll plugins.
        /// References to the loaded commands are stored in a dispatcher Dictionary.
        /// </summary>
        /// <param name="commandNames">List of commands to load</param>
        public void LoadPlugins(List<string> commandNames)
        {
            foreach (string  commandName in commandNames)
            {
                if (!IsCommandLoaded(commandName))
                {
                    Console.WriteLine("Loading plugin: " + GetPluginFilename(commandName));
                    var command = LoadPlugin(GetPluginFilename(commandName));
                    dispatcher.Add(commandName, command);
                }
            }
        }

        /// <summary>
        /// Returns the corresponding command object for a command name
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>A command object</returns>
        /// <exception cref="System.MissingMethodException">Thrown when the command is not loaded.</exception>
        public PluginInterface.ICommand GetCommand(string commandName)
        {
            if (!IsCommandLoaded(commandName))
            {
                throw new MissingMethodException("This command is not known. Missing plugin?");
            }
            return dispatcher[commandName];
        }


        /// <summary>
        /// Checks if the input commands have been loaded from plugins.
        /// </summary>
        /// <param name="commandNames">List of command names to check</param>
        /// <returns>
        /// False if any plugin is missing. 
        /// True if all plugins are loaded.
        /// </returns>
        public bool ArePluginsLoaded(List<string> commandNames)
        {
            foreach (string commandName in commandNames)
            {
                if (!IsCommandLoaded(commandName))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Loads a plugin from the plugin folder.
        /// </summary>
        /// <param name="pluginName">Plugin to load</param>
        /// <returns>An instance of the plugin</returns>
        /// <exception cref="System.Exception">
        /// Thrown when the .dll does not implement ICommand
        /// </exception>
        private PluginInterface.ICommand LoadPlugin(string pluginName)
        {
            string pluginPath = paths.PluginsFolder + pluginName;

            Assembly assembly = Assembly.LoadFile(pluginPath);
            foreach (Type item in assembly.GetTypes())
            {
                if (!item.IsClass)
                {
                    continue;
                }
                if (item.GetInterfaces().Contains(typeof(PluginInterface.ICommand)))
                {
                    return (PluginInterface.ICommand)Activator.CreateInstance(item);
                }
            }
            throw new Exception("Invalid DLL, Interface not found!");
        }

        private string GetPluginFilename(string pluginName)
        {
            return "Command_" + pluginName + ".dll";
        }

        private bool IsCommandLoaded(string commandName)
        {
            return dispatcher.ContainsKey(commandName);
        }
    }
}
