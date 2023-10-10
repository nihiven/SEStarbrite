using System;
using System.Collections.Generic;
using System.Linq;


namespace Preditor
{
    public class Stardust
    {
        public List<CommandMapping> _commandMap;

        public Stardust() 
        {
            _commandMap = new List<CommandMapping>();
            _commandMap.Add(new CommandMapping("jb", cmd_jb));
        }


        public bool ProcessInput(string _input)
        {
            var _command = GetCommandMappingByName(_input.Split(" ").Take(1).ToString());

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

        public void MapCommand(string _command, string _parameters)
        {



        }


        /// <summary>
        ///  Baby's first Console Command
        /// </summary>
        /// <param name="_parameters"></param>
        /// <param name="_test"></param>
        public void cmd_jb(CommandParameter commandParameters)
        {
            Console.WriteLine(commandParameters.ToString());
        }
    }

    // #1: sets <name> value
    // #1: seti <name> value
    // #1: setb <name> value

    // #2: isOption(name)
    //  Yes: setOption<type>(name, value)
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


}
