using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerApp
{
    /// <summary>
    /// Interface implemented by script readers.
    /// </summary>
    interface IScriptReader
    {
        Script LoadFromFile(string scriptFilename);
    }
}
