using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using Unity;
using Unity.Lifetime;

/// <summary>
/// This sequencer is a proof of concept for a pluggable sequencer.
/// Functionality:
///   - loads the sequence to be executed from an.xml file
///	  - loads the commands (pluggins) from .dll files
///	  - executes the commands as described in the.xml file
/// </summary>
namespace SequencerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Unity dependency injection
            var container = new UnityContainer();

            container.RegisterType<IPaths, DefaultPaths>();
            container.RegisterType<IScriptReader, XmlScriptReader>();
            container.RegisterType<IPluginsManager, PluginsManager>(new ContainerControlledLifetimeManager());

            IPaths paths = container.Resolve<IPaths>();
            IScriptReader reader = container.Resolve<IScriptReader>();
            IPluginsManager pluginsManager = container.Resolve<IPluginsManager>();
            Sequencer sequencer = container.Resolve<Sequencer>();
            // ---------------------------------------------

            try
            {
                Console.WriteLine("\n ---------- READING SEQUENCE FILE -------- ");
                Script script = reader.LoadFromFile(paths.ScriptsFolder + "sequence1.xml");
                Console.WriteLine(script.ToString());

                Console.WriteLine("\n ------------ LOADING PLUGINS ---------- ");
                pluginsManager.LoadPlugins(script.GetUniqueListOfCommands()); 


                Console.WriteLine("\n ------------ EXECUTING SEQUENCE --------- ");
                sequencer.Execute(script);

            }
            catch (Exception ex)
            {
                Console.WriteLine("\nEXCEPTION: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
