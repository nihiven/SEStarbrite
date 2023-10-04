using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Preditor
{
    public class StarbriteOption
    {
        public string Type;
        public string Name;
        public string Description;

        public StarbriteOption(string _type, string _name, string? _description = "")
        {
            Type = _type;
            Name = _name;
            Description = _description;
        }

    }

    public class StarbriteOptionString : StarbriteOption
    {
        public string Value;
        public StarbriteOptionString(string _name, string _value, string? _description) : base("string", _name, _description) 
        {
            Value = _value;
        }
    }

    public class StarbriteOptionInt : StarbriteOption
    {
        public int Value;
        public StarbriteOptionInt(string _name, int _value, string? _description) : base("int", _name, _description)
        {
            Value = _value;
        }
    }

    public class Starbrite
    {
        private Color _colorBackground = Color.Fuchsia;
        private string _homeDirectory = ".";
        private string _configFilter = "*.ses";
        private IEnumerable<string> _configFiles;
        public Color ColorBackground => _colorBackground;
        public string HomeDirectory => _homeDirectory;
        public IEnumerable<string> ConfigFileArray => _configFiles;

        private List<StarbriteOption> _options;
        public List<StarbriteOption> Options => _options;


        public Starbrite() 
        { 
            _configFiles = new string[] { };

            _options = new List<StarbriteOption>();
            _options.Add(new StarbriteOptionString("Server", "irc.uncle.moe", "The main server to connect to."));
            _options.Add(new StarbriteOptionInt("Port", 2223, "The server port to connect to."));
        }

        public int ListConfigFiles()
        {
            _configFiles = new string[] { };
            try
            {
                // enumerate home directory for .ses files
                var _searchFiles = Directory.EnumerateFiles(_homeDirectory, _configFilter, SearchOption.AllDirectories);

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
    }
}
