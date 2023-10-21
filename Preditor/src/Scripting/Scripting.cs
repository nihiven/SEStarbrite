﻿using MoonSharp.Interpreter;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Preditor
{
    public class Stardust
    {
        private Script _script;

        // external references
        private Starbrite _engine;
        private VariableStore _variables;

        public Stardust(Starbrite engine, VariableStore variables)
        {
            Log.Debug("Stardust startup");

            // use CoreModules to limit Lua access to features
            // CoreModules modules = CoreModules.Basic
            // then pass them to Script
            _engine = engine;
            _variables = variables;
            
            luaSetup();
        }

        private void luaSetup()
        {
            // setup lua options
            Script.DefaultOptions.DebugPrint = s => Log.Debug("[LUA] " + s);

            // our API script instance
            _script = new Script();


            // bind functions to lua script calls
            _script.Globals["add"] = (Action<string, string, string, bool>)_variables.Add;
            _script.Globals["set"] = (Func<string, string, bool>)_variables.Set;

            string scriptCode = @"    
        function dust(str)
            add('luaVar', 'this was set in lua', 'STARSCREAM', false)
            set('luaVar', str)
            print('eat a burger')
        end
";
            _script.DoString(scriptCode);
        }

        public void MoonTest()
        {
            _script.Call(_script.Globals["dust"], "called from MoonTest()");
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
            foreach (var file in ConfigFileArray)
            {

            }
        }
    }
}
