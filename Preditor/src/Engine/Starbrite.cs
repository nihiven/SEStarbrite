﻿using Microsoft.Xna.Framework;
using Preditor.Engine;
using Serilog;
using System;

// ?
// https://learn-monogame.github.io/how-to/automate-release/

namespace Preditor
{
    public class Starbrite : StarbriteGame
    {
        // engine components
        private VariableStore _variables;
        private EventDispatcher _events;

        // accessors
        public VariableStore Variables => _variables;
        
        // Testing
        public string GetVariable(Variable variable) => _variables.GetVariableValue(variable);
        public string GetVariable(string name) => _variables.GetVariableValueByName(name);

        // expose some of the hard coded options as properties
        public string VersionString => _variables.GetVariableValueByName("versionString");
        public string IsBeta => _variables.GetVariableValueByName("betaBuild");
        public string DirHome => _variables.GetVariableValueByName("dirHome");
        public string FileConfigFilter => _variables.GetVariableValueByName("fileConfigFilter");

        public Starbrite(EventDispatcher events) 
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("Starbrite startup");

            // internal components
            _events = events;
            _variables = new VariableStore();

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

        protected override void Draw(GameTime gameTime)
        {
            
        }

    }
}
