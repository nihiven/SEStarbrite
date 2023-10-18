* Let's get Lua running before we get too far into coding the API

* Variable naming
 - do variables even need types?
 - do we give an option to display variable names with a type indicator?
  -- coolVarl -> coolVar[int] coolVar[bool] coolVar[string] coolVar[float]
* Scripting
 - for now 'scripting' is just a way to run a script file
 - in the future, can we implement Lua scripting?
 - need to be able to reference variables in a script/command -> {coolVar} ?

* Namespaces and class names need some touching up.

cEditor (editor)
 - cStarbrite (engine)
  - cStarscream (API)
  - cStardust (scripting)
   - cScriptFunctions
  - cVariableStore

Preditor
 Engine
  VariableStore
 Editor
 Scripting
  Functions
 API

.....