using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_B
{
    public class CommandB : PluginInterface.ICommand
    {
        public bool Init(object[] args)
        {
            Console.WriteLine("CommandB: Init(" + string.Join(",", args) + ") called");
            return true;
        }

        public void Execute(object[] args)
        {
            Console.WriteLine("CommandB: Execute(" + string.Join(",", args) + ") called");
        }

        public bool DeInit()
        {
            Console.WriteLine("CommandB: DeInit() called");
            return true;
        }
    }
}
