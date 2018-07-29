using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_A
{
    public class CommandA : PluginInterface.ICommand
    {
        public bool Init(object[] args)
        {
            Console.WriteLine("CommandA: Init(" + string.Join(",", args) + ") called");
            return true;
        }

        public void Execute(object[] args)
        {
            Console.WriteLine("CommandA: Execute(" + string.Join(",", args) + ") called" );
        }

        public bool DeInit()
        {
            Console.WriteLine("CommandA: DeInit() called");
            return true;
        }
    }
}
