using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_C
{
    public class CommandC : PluginInterface.ICommand
    {
        public bool Init(object[] args)
        {
            Console.WriteLine("CommandC: Init(" + string.Join(",", args) + ") called");
            return true;
        }

        public void Execute(object[] args)
        {
            Console.WriteLine("CommandC: Execute(" + string.Join(",", args) + ") called");
        }

        public bool DeInit()
        {
            Console.WriteLine("CommandC: DeInit() called");
            return true;
        }
    }
}
