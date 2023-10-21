namespace Preditor
{
    public class ScriptFunctions
    {
        private VariableStore _options;

        public ScriptFunctions(VariableStore variables)
        {
            _options = variables;
        }

        // all internal commands should be private
        public void cmd_jb(CommandParameter commandParameters)
        {
            _options.Add("coolCommandTest", "This is being set from cmd_jb", "Sixty niiine!", false);
        }

        public void cmd_set(CommandParameter commandParameter)
        {
            _options.Set(commandParameter._parametersSplit[1], commandParameter._parametersSplit[2]);
        }
    }

}

