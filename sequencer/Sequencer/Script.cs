using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerApp
{
    /// <summary>
    /// Script data is store in this class.
    /// </summary>
    class Script
    {
        /// <summary>
        /// Init() commands are stored here as a list of steps.
        /// </summary>
        public List<ScriptStep> initCommands;

        /// <summary>
        /// Execute() commands are stored here as a list of steps.
        /// </summary>
        public List<ScriptStep> executeCommands;

        /// <summary>
        /// DeInit() commands are stored here as a list of steps.
        /// </summary>
        public List<ScriptStep> deInitCommands;

         
        public Script()
        {
            initCommands = new List<ScriptStep>();
            executeCommands = new List<ScriptStep>();
            deInitCommands = new List<ScriptStep>();
        }

        public override string ToString()
        {
            return ToStringFromListOfSteps("init commands:", initCommands) 
                   + ToStringFromListOfSteps("execute commands:", executeCommands) 
                   + ToStringFromListOfSteps("deinit commands:", deInitCommands);
        }

        public List<string> GetUniqueListOfCommands()
        {
            var allCommands = initCommands.Union(executeCommands).Union(deInitCommands);
            return allCommands.Select(o => o.CommandName).Distinct().ToList(); ;
        }

        private string ToStringFromListOfSteps(string title, List<ScriptStep> list)
        {
            string message = Environment.NewLine + title + Environment.NewLine;
            foreach (ScriptStep step in list)
            {
                message += "  " + step.ToString() + Environment.NewLine;
            }
            return message;
        }
    }
}
