namespace Preditor
{
    public class ScriptFunctions
    {
        private Starbrite _engine;

        public ScriptFunctions(Starbrite engine)
        {
           _engine = engine;
        }

        // all internal commands should be private
        public void cmd_jb(CommandParameter commandParameters)
        {
            _engine.Variables.Add("coolCommandTest", "This is being set from cmd_jb", "Sixty niiine!", false);
        }

        public void cmd_set(CommandParameter commandParameter)
        {
            _engine.Variables.Set(commandParameter._parametersSplit[1], commandParameter._parametersSplit[2]);
        }
    }

}

