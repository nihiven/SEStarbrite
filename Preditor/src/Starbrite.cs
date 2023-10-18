using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ?
// https://learn-monogame.github.io/how-to/automate-release/

namespace Preditor
{
    public class Starbrite
    {
        // engine components
        private Stardust _scripting;
        private Starscream _api;
        private VariableStore _variables;

        // move to starscream
        private List<string> _configFiles;

        // accessors
        public List<Variable> Variables => _variables.Variables;
        public List<string> ConfigFileArray => _configFiles;

        // expose some of the hard coded options as properties
        public string VersionString => GetVariableValueByName("versionString");
        public string IsBeta => GetVariableValueByName("betaBuild");
        public string DirHome => GetVariableValueByName("dirHome");
        public string FileConfigFilter => GetVariableValueByName("fileConfigFilter");

        public Starbrite() 
        { 
            _configFiles = new List<string>();
            _variables = new VariableStore();
            _api = new Starscream(_variables);

            // system - version
            _variables.Add("engineName", "Engine name", "Starbrite", true);
            _variables.Add("versionName", "Engine version code name", "Aliens", true);
            _variables.Add("versionMajor", "Engine major version", 0, true);
            _variables.Add("versionMinor", "Engine minor version", 1, true);
            _variables.Add("versionRevision", "Engine revision version", 0, true);
            _variables.Add("betaBuild", "Is this a beta build of Starbrite?", true, true);
            _variables.Add("versionString", 
                                "Engine version string", 
                                String.Format("{0} - v{1}.{2}.{3} - \"{4}\"", GetVariableValueByName("engineName"), GetVariableValueByName("versionMajor"), GetVariableValueByName("versionMinor"), GetVariableValueByName("versionRevision"), GetVariableValueByName("versionName")), 
                                true);

            // system - script related
            _variables.Add("dirHome", "Home data directory", ".", false);
            _variables.Add("fileConfigFilter", "Home data directory", "*.ses", false);
            _variables.Add("gameTimeScale", "World timescale, relative to normal time.", 1.0f, false);


            // test related
            _variables.Add("toggleTest", "Toggle to test Set() implementation", false, false);
        }

        public int ListConfigFiles()
        {
            _variables.Set("toggleTest", true);

            try
            {
                // enumerate home directory for .ses files
                var _searchFiles = Directory.EnumerateFiles(DirHome, FileConfigFilter, SearchOption.AllDirectories);

                foreach (string _currentFile in _searchFiles)
                {
                    _configFiles.Add(_currentFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return _configFiles.Count();
        }

        public void ScanConfigFiles()
        {
            foreach(var file in ConfigFileArray)
            {

            }
        }

        public string GetVariableValue(Variable variable)
        {
            if (variable == null) return "ERROR: GetOptionValue (null option)";

            switch (variable.Type)
            {
                case "string":
                    var os = (VariableString)variable;
                    return os.Value;
                case "int":
                    var oi = (VariableInt)variable;
                    return oi.Value.ToString();
                case "bool":
                    var ob = (VariableBool)variable;
                    return ob.Value.ToString();
                case "float":
                    var of = (VariableFloat)variable;
                    return of.Value.ToString();
            }

            return "ERROR: GetOptionValue";
        }

        public string GetVariableValueDefault(Variable variable)
        {
            switch (variable.Type)
            {
                case "string":
                    var os = (VariableString)variable;
                    return os.ValueDefault;
                case "int":
                    var oi = (VariableInt)variable;
                    return oi.ValueDefault.ToString();
                case "bool":
                    var ob = (VariableBool)variable;
                    return ob.ValueDefault.ToString();
                case "float":
                    var of = (VariableFloat)variable;
                    return of.ValueDefault.ToString();
            }

            return "ERROR: GetOptionValueDefault";
        }

        public string GetVariableValueByName(string _name)
        {
            return GetVariableValue(_variables.Get(_name));  
        }


    }
}
