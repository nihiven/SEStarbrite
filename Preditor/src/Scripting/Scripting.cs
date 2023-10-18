using MoonSharp.Interpreter;

namespace Preditor
{
    public class Stardust
    {
        private Script _script;
        private VariableStore _variables;

        public Stardust(VariableStore variables)
        {
            // use CoreModules to limit Lua access to features
            // CoreModules modules = CoreModules.Basic
            // then pass them to Script

            _variables = variables;
            _script = new Script();
        }

        public void moonTest()
        {
            string scriptCode = @"    
        -- defines a factorial function
        function fact (n)
            if (n == 0) then
                return 1
            else
                return n * fact(n - 1);
            end
        end";

            Script script = new Script();

            script.DoString(scriptCode);

            DynValue res = script.Call(script.Globals["fact"], 4);

            return res.Number;
        }
    }
}
