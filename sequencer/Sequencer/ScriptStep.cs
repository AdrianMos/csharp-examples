using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerApp
{
    /// <summary>
    /// ScriptStep class is used for storing and passing information about a command
    /// </summary>
    class ScriptStep
    {
        public string CommandName { get; }
        public string[] CommandParameters { get; }

        public ScriptStep(string commandName, string[] commandParameters)
        {
            this.CommandName = commandName;
            this.CommandParameters = commandParameters;
        }

        public override string ToString()
        {
            return "Name=" + CommandName + " Parameters=[" + string.Join(",", CommandParameters) + "]";
        }
    }
}