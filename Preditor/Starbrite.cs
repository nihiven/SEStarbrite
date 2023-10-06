using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Preditor
{
    public class StarbriteOption
    {
        public string Type;
        public string Name;
        public string Description;
        public readonly bool Protected;

        public StarbriteOption(string _type, string _name, bool _protected, string? _description = "")
        {
            Type = _type;
            Name = _name;
            Description = _description;
            Protected = _protected;
        }

    }

    public class StarbriteOptionString : StarbriteOption
    {
        public string Value;
        public StarbriteOptionString(string _name, string _value, bool _protected, string? _description) : base("string", _name, _protected, _description) 
        {
            Value = _value;
        }
    }

    public class StarbriteOptionInt : StarbriteOption
    {
        public int Value;
        public StarbriteOptionInt(string _name, int _value, bool _protected, string? _description) : base("int", _name, _protected, _description)
        {
            Value = _value;
        }
    }

    public class StarbriteOptionBool : StarbriteOption
    {
        public bool Value;
        public StarbriteOptionBool(string _name, bool _value, bool _protected, string? _description) : base("bool", _name, _protected, _description)
        {
            Value = _value;
        }
    }

    public class Starbrite
    {
        private IEnumerable<string> _configFiles;
        private List<StarbriteOption> _options;

        /// <summary>
        ///  publicly exposed
        /// </summary>
        public List<StarbriteOption> Options => _options;
        public IEnumerable<string> ConfigFileArray => _configFiles;
        // expose some of the hard coded options as properties
        public string VersionString => GetOptionValueByName("versionString");
        public string IsBeta => GetOptionValueByName("betaBuild");
        public string DirHome => GetOptionValueByName("dirHome");
        public string FileConfigFilter => GetOptionValueByName("fileConfigFilter");

        public Starbrite() 
        { 
            _configFiles = new string[] { };
            _options = new List<StarbriteOption>();

            // system - version
            _options.Add(new StarbriteOptionString("engineName",    "Starbrite",    true, "Engine name"));
            _options.Add(new StarbriteOptionString("versionName",   "Aliens",       true, "Engine version code name"));
            _options.Add(new StarbriteOptionInt("versionMajor",     0,              true, "Engine major version"));
            _options.Add(new StarbriteOptionInt("versionMinor",     1,              true, "Engine minor version"));
            _options.Add(new StarbriteOptionInt("versionRevision",  0,              true, "Engine revision version"));
            _options.Add(new StarbriteOptionBool("betaBuild",       true,           true, "Is this a beta build of Starbrite?"));
            _options.Add(new StarbriteOptionString(
                                "versionString",
                                String.Format("{0} - v{1}.{2}.{3} - \"{4}\"", GetOptionValueByName("engineName"), GetOptionValueByName("versionMajor"), GetOptionValueByName("versionMinor"), GetOptionValueByName("versionRevision"), GetOptionValueByName("versionName")), 
                                true, 
                                "Engine version string"));

            // system - script related
            _options.Add(new StarbriteOptionString("dirHome", ".", false, "Home data directory"));
            _options.Add(new StarbriteOptionString("fileConfigFilter", "*.ses", false, "Home data directory")); // cofig extension instead? *.ses


            // junko
            _options.Add(new StarbriteOptionString("Server","irc.uncle.moe", false, "The main server to connect to."));
            _options.Add(new StarbriteOptionInt("Port", 2223, false, "The server port to connect to."));
        }

        public int ListConfigFiles()
        {
            _configFiles = new string[] { };
            try
            {
                // enumerate home directory for .ses files
                var _searchFiles = Directory.EnumerateFiles(DirHome, FileConfigFilter, SearchOption.AllDirectories);

                foreach (string _currentFile in _searchFiles)
                {
                    _configFiles = _configFiles.Append(_currentFile);
                }
            }
            catch (Exception e)
            {

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
            }

            return "ERROR: GetOptionValue";
        }

        public StarbriteOption GetOptionByName(string _name)
        {
            return _options.Find(x => x.Name == _name);
        }

        public string GetOptionValueByName(string _name)
        {
            return GetOptionValue(GetOptionByName(_name));  
        }

        public string GetVersionString()
        {
            return String.Format("{0} - v{1}.{2}.{3} - \"{4}\"", GetOptionValueByName("engineName"), GetOptionValueByName("versionMajor"), GetOptionValueByName("versionMinor"), GetOptionValueByName("versionRevision"), GetOptionValueByName("versionName"));
        }
    }
}
