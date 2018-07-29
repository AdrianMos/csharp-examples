using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace SequencerApp
{
    /// <summary>
    /// Reads scripts from XML file format
    /// </summary>
    class XmlScriptReader : IScriptReader
    {
        private XmlDocument xmlDoc;

        public XmlScriptReader()
        {
            xmlDoc = new XmlDocument();
        }

        /// <summary>
        /// Loads script data from an XML file.
        /// </summary>
        /// <param name="scriptFile">Input file</param>
        /// <returns>The script object</returns>
        public Script LoadFromFile(string scriptFile)
        {
            Script script = new Script();

            xmlDoc.Load(scriptFile);

            script.initCommands = GetStepsFromSection("Script/Init/*");
            script.executeCommands = GetStepsFromSection("Script/Sequence/*");
            script.deInitCommands = GetStepsFromSection("Script/DeInit/*");
           
            return script;
        }

        /// <summary>
        /// Extracts all command steps from a section.
        /// </summary>
        /// <param name="xpath">xpath of the section</param>
        /// <returns>A list containing the extracted steps</returns>
        private List<ScriptStep> GetStepsFromSection(string xpath)
        {
            List<ScriptStep> steps = new List<ScriptStep>();
            var nodes = xmlDoc.SelectNodes(xpath);
            
            foreach (XmlNode childNode in nodes)
            {
                string[] parameters = GenerateParametersArrayFromNodeAttributes(childNode.Attributes);

                steps.Add(new ScriptStep(childNode.Name, parameters));
            }
            return steps;
        }

        /// <summary>
        /// Generates the parameters array from a node's attributes.
        /// e.g. A Param2="InitParamA2" Param1='InitParamA1'/> => ["InitParamA1", "InitParamA2"]
        /// </summary>
        /// <param name="attributes">xml node attributes</param>
        /// <returns>An array of strings containing the parameters values</returns>
        private string[] GenerateParametersArrayFromNodeAttributes(XmlAttributeCollection attributes)
        {
            string[] parametersArray = new string[GetMaxParameterIndex(attributes)];

            for (int i=0; i<parametersArray.Length; i++)
                parametersArray[i] = "";

            foreach (XmlAttribute attribute in attributes)
            {
                int argumentNumber = GetParameterIndex(attribute.Name);
                parametersArray[argumentNumber-1] = attribute.Value;
            }
            return parametersArray;
        }

        /// <summary>
        /// Extracts the index of a parameter, e.g. Param12 => 12
        /// </summary>
        /// <param name="parameter">Parameter as string</param>
        /// <returns>Index of the parameter</returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown when an invalid parameter format is detected.
        /// </exception>
        private int GetParameterIndex(string parameter)
        {
            Regex regex = new Regex(@"(^Param)(\d+)");
            Match match = regex.Match(parameter);

            if (!match.Success)
                throw new ArgumentException("Invalid parameter format: " + parameter);
            
            return int.Parse(match.Groups[2].Value);
        }

        /// <summary>
        /// Finds the maximum index of the parameters
        /// e.g. Param20="InitParamA", Param1='InitParamA1'/> => 20
        /// We need this so that we know the size of the parameters array.
        /// </summary>
        /// <param name="attributes">xml node attributes</param>
        /// <returns>Maximum parameter index</returns>
        private int GetMaxParameterIndex(XmlAttributeCollection attributes)
        {
            int max = 0;
            foreach (XmlAttribute attribute in attributes)
            {
                int index = GetParameterIndex(attribute.Name);
                if (index > max)
                    max = index;
            }
            return max;
        }

    }
}
