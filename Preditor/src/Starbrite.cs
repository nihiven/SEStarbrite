using Serilog;
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

        // Testing
        public void LuaTest() => _scripting.MoonTest();
        public string GetVariableValue(Variable variable) => _variables.GetVariableValue(variable);

        // expose some of the hard coded options as properties
        public string VersionString => _variables.GetVariableValueByName("versionString");
        public string IsBeta => _variables.GetVariableValueByName("betaBuild");
        public string DirHome => _variables.GetVariableValueByName("dirHome");
        public string FileConfigFilter => _variables.GetVariableValueByName("fileConfigFilter");

        public Starbrite() 
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("Starbrite startup");

            _configFiles = new List<string>();
            _variables = new VariableStore();
            _scripting = new Stardust(_variables);
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
                                String.Format("{0} - v{1}.{2}.{3} - \"{4}\"", _variables.GetVariableValueByName("engineName"), _variables.GetVariableValueByName("versionMajor"), _variables.GetVariableValueByName("versionMinor"), _variables.GetVariableValueByName("versionRevision"), _variables.GetVariableValueByName("versionName")), 
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
                    Log.Debug("Found config file: {0}", _currentFile);
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

    }
}
