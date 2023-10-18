using System.Collections.Generic;

namespace Preditor
{
    // option types
    public class Variable
    {
        public string Type;
        public string Name;
        public string Description;
        public readonly bool Protected;

        public Variable(string _type, string _name, string _description, bool _protected)
        {
            Type = _type;
            Name = _name;
            Description = _description;
            Protected = _protected;
        }
    }
    public class VariableString : Variable
    {
        public string Value;
        public readonly string ValueDefault;
        public VariableString(string _name, string _description, string _value, bool _protected) : base("string", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }
    public class VariableInt : Variable
    {
        public int Value;
        public readonly int ValueDefault;
        public VariableInt(string _name, string _description, int _value, bool _protected) : base("int", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }
    public class VariableBool : Variable
    {
        public bool Value;
        public readonly bool ValueDefault;
        public VariableBool(string _name, string _description, bool _value, bool _protected) : base("bool", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }
    public class VariableFloat : Variable
    {
        public float Value;
        public readonly float ValueDefault;
        public VariableFloat(string _name, string _description, float _value, bool _protected) : base("float", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }

    public class VariableStore
    {
        // private stuff
        private List<Variable> _variables;

        // public stuff
        public List<Variable> Variables => _variables; // rename to All()?
        public Variable Get(string name) { return _variables.Find(x => x.Name == name); }


        public VariableStore() 
        { 
            _variables = new List<Variable>();
        }

        //  TODO: remove add and update set to create variables
        // add overload for each type
        public void Add(string _name, string _description, string _value, bool _protected)
        {
            _variables.Add(new VariableString(_name, _description, _value, _protected));
        }
        public void Add(string _name, string _description, int _value, bool _protected)
        {
            _variables.Add(new VariableInt(_name, _description, _value, _protected));
        }
        public void Add(string _name, string _description, bool _value, bool _protected)
        {
            _variables.Add(new VariableBool(_name, _description, _value, _protected));
        }
        public void Add(string _name, string _description, float _value, bool _protected)
        {
            _variables.Add(new VariableFloat(_name, _description, _value, _protected));
        }

        // set overload for each type
        public bool Set(string _name, string _value)
        {
            var _option = Get(_name);
            if (_option != null)
            {
                if (_option.Type == "string")
                {
                    (_option as VariableString).Value = _value;
                    return true;
                }
            }
            return false;
        }
        public bool Set(string _name, int _value)
        {
            var _option = Get(_name);
            if (_option != null)
            {
                if (_option.Type == "int")
                {
                    (_option as VariableInt).Value = _value;
                    return true;
                }
            }
            return false;
        }
        public bool Set(string _name, bool _value)
        {
            var _option = Get(_name);
            if (_option != null)
            {
                if (_option.Type == "bool")
                {
                    (_option as VariableBool).Value = _value;
                    return true;
                }
            }
            return false;
        }
        public bool Set(string _name, float _value)
        {
            var _option = Get(_name);
            if (_option != null)
            {
                if (_option.Type == "float")
                {
                    (_option as VariableFloat).Value = _value;
                    return true;
                }
            }
            return false;
        }

        // get methods

    }
}
