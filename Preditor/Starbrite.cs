using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Preditor
{
    public class Starbrite
    {
        private Color _colorBackground = Color.Fuchsia;
        private string _homeDirectory = "."; // AppContext.BaseDirectory works but relative is better
        private string _configFilter = "*.ses";
        private IEnumerable<string> _configFiles;
        public Color ColorBackground => _colorBackground;
        public string HomeDirectory => _homeDirectory;
        public IEnumerable<string> ConfigFileArray => _configFiles;


        public Starbrite() 
        { 
            _configFiles = new string[] { };
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
