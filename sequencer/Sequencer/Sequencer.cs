using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SequencerApp
{
    /// <summary>
    /// The Sequencer executes the script commands.
    /// It uses the IPluginManager for getting access to the commands.
    /// </summary>
    class Sequencer
    {
        private IPluginsManager pluginsManager;


        public Sequencer(IPluginsManager pluginsManager)
        {
            this.pluginsManager = pluginsManager;
        } 


        /// <summary>
        /// Executes a script.
        /// </summary>
        /// <param name="script">Script to execute</param>
        /// <exception cref="System.InvalidProgramException">
        /// Thrown when a command within the script doesn't have the corresponding plugin loaded.
        /// </exception>
        public void Execute(Script script)
        {
            if (!pluginsManager.ArePluginsLoaded(script.GetUniqueListOfCommands()))
                throw new InvalidProgramException("Cannot execute the script. Some plugins are not loaded!");

            foreach (ScriptStep step in script.initCommands)
            {
                var command = pluginsManager.GetCommand(step.CommandName);
                command.Init(step.CommandParameters);
            }
                
            foreach (ScriptStep step in script.executeCommands)
            {
                var command = pluginsManager.GetCommand(step.CommandName);
                command.Execute(step.CommandParameters);
            }
            
            foreach (ScriptStep step in script.deInitCommands)
            {
                var command = pluginsManager.GetCommand(step.CommandName);
                command.DeInit();
            }

        }

    }
}
