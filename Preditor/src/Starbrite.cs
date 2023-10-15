using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Preditor
{
    public class Starbrite
    {
        private Stardust _mapper;
        private StarbriteOptions _options;
        private List<string> _configFiles;

        public StarbriteOptions OptionStore => _options;
        public List<string> ConfigFileArray => _configFiles;

        // expose some of the hard coded options as properties
        public string VersionString => GetOptionValueByName("versionString");
        public string IsBeta => GetOptionValueByName("betaBuild");
        public string DirHome => GetOptionValueByName("dirHome");
        public string FileConfigFilter => GetOptionValueByName("fileConfigFilter");

        public Starbrite() 
        { 
            _configFiles = new List<string>();
            _options = new StarbriteOptions();

            _mapper = new Stardust(_options);

            // system - version
            _options.Add("engineName", "Engine name", "Starbrite", true);
            _options.Add("versionName", "Engine version code name", "Aliens", true);
            _options.Add("versionMajor", "Engine major version", 0, true);
            _options.Add("versionMinor", "Engine minor version", 1, true);
            _options.Add("versionRevision", "Engine revision version", 0, true);
            _options.Add("betaBuild", "Is this a beta build of Starbrite?", true, true);
            _options.Add("versionString", 
                                "Engine version string", 
                                String.Format("{0} - v{1}.{2}.{3} - \"{4}\"", GetOptionValueByName("engineName"), GetOptionValueByName("versionMajor"), GetOptionValueByName("versionMinor"), GetOptionValueByName("versionRevision"), GetOptionValueByName("versionName")), 
                                true);

            // system - script related
            _options.Add("dirHome", "Home data directory", ".", false);
            _options.Add("fileConfigFilter", "Home data directory", "*.ses", false);
            _options.Add("gameTimeScale", "World timescale, relative to normal time.", 1.0f, false);



            // test related
            _options.Add("toggleTest", "Toggle to test Set() implementation", false, false);

            StarbriteOption _testString = _options.Get("toggleTest");
        }

        public int ListConfigFiles()
        {
            _options.Set("toggleTest", true);

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

        public string GetOptionValue(StarbriteOption _option)
        {
            if (_option == null) return "ERROR: GetOptionValue (null option)";

            switch (_option.Type)
            {
                case "string":
                    var os = (StarbriteOptionString)_option;
                    return os.Value;
                case "int":
                    var oi = (StarbriteOptionInt)_option;
                    return oi.Value.ToString();
                case "bool":
                    var ob = (StarbriteOptionBool)_option;
                    return ob.Value.ToString();
                case "float":
                    var of = (StarbriteOptionFloat)_option;
                    return of.Value.ToString();
            }

            return "ERROR: GetOptionValue";
        }

        public string GetOptionValueDefault(StarbriteOption _option)
        {
            switch (_option.Type)
            {
                case "string":
                    var os = (StarbriteOptionString)_option;
                    return os.ValueDefault;
                case "int":
                    var oi = (StarbriteOptionInt)_option;
                    return oi.ValueDefault.ToString();
                case "bool":
                    var ob = (StarbriteOptionBool)_option;
                    return ob.ValueDefault.ToString();
                case "float":
                    var of = (StarbriteOptionFloat)_option;
                    return of.ValueDefault.ToString();
            }

            return "ERROR: GetOptionValueDefault";
        }

        public string GetOptionValueByName(string _name)
        {
            return GetOptionValue(_options.Get(_name));  
        }


    }
}
