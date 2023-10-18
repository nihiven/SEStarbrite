using System;
using System.Collections.Generic;
using System.Linq;


namespace Preditor
{
    public class CommandParameter
    {
        public string _parameters;
        public string[] _parametersSplit;

        public CommandParameter(string parameters)
        {
            _parameters = parameters;
            _parametersSplit = parameters.Split(" ");
        }
    }
    public class CommandMapping
    {
        public string Command;
        public Action<CommandParameter> Function;

        public CommandMapping(string command, Action<CommandParameter> function)
        {
            Command = command;
            Function = function;
        }
    }

    // stardust is the command mapper for Starbrite
    // stardust takes input as a string, and maps it to a function
    // stardust will need access to starbrite's internals, such as the option store
    public class Starscream
    {
        private VariableStore _options;
        private ScriptFunctions _functions;
            
        public List<CommandMapping> _commandMap;

        public Starscream(VariableStore options) 
        {
            _options = options;

            _functions = new ScriptFunctions(_options);
            _commandMap = new List<CommandMapping>();

            // add commands
            this.Add("jb", _functions.cmd_jb); // test command
            this.Add("set", _functions.cmd_set);

            ProcessInput("jb is running a test");
        }

        public void Add(string _command, Action<CommandParameter> _function)
        {
            _commandMap.Add(new CommandMapping(_command, _function));
        }

        // create a ProcessInputResult class that contains a bool for success, and a string for error message?
        // shoud go in starscream
        public bool ProcessInput(string _input)
        {
            string _commandText = _input.Split(" ")[0];
            CommandMapping _command = GetCommandMappingByName(_commandText);

            if (_command == null)
            {
                return false;
            }

            CommandParameter _cmdParm = new CommandParameter(_input);

            _command.Function(_cmdParm);
            return true;
        }

        public CommandMapping GetCommandMappingByName(string _name)
        {
            return _commandMap.Where(command => command.Command == _name).FirstOrDefault();
        }

    }
}
