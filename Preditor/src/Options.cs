using System.Collections.Generic;


namespace Preditor
{
    // option types
    public class StarbriteOption
    {
        public string Type;
        public string Name;
        public string Description;
        public readonly bool Protected;

        public StarbriteOption(string _type, string _name, string _description, bool _protected)
        {
            Type = _type;
            Name = _name;
            Description = _description;
            Protected = _protected;
        }
    }
    public class StarbriteOptionString : StarbriteOption
    {
        public string Value;
        public readonly string ValueDefault;
        public StarbriteOptionString(string _name, string _description, string _value, bool _protected) : base("string", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }
    public class StarbriteOptionInt : StarbriteOption
    {
        public int Value;
        public readonly int ValueDefault;
        public StarbriteOptionInt(string _name, string _description, int _value, bool _protected) : base("int", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }
    public class StarbriteOptionBool : StarbriteOption
    {
        public bool Value;
        public readonly bool ValueDefault;
        public StarbriteOptionBool(string _name, string _description, bool _value, bool _protected) : base("bool", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }
    public class StarbriteOptionFloat : StarbriteOption
    {
        public float Value;
        public readonly float ValueDefault;
        public StarbriteOptionFloat(string _name, string _description, float _value, bool _protected) : base("float", _name, _description, _protected)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }

    public class StarbriteOptions
    {
        // private stuff
        private List<StarbriteOption> _options;

        // public stuff
        public List<StarbriteOption> Options => _options;
        public StarbriteOption Get(string name) { return _options.Find(x => x.Name == name); }


        public StarbriteOptions() 
        { 
            _options = new List<StarbriteOption>();
        }

        // add overload for each type
        public void Add(string _name, string _description, string _value, bool _protected)
        {
            _options.Add(new StarbriteOptionString(_name, _description, _value, _protected));
        }
        public void Add(string _name, string _description, int _value, bool _protected)
        {
            _options.Add(new StarbriteOptionInt(_name, _description, _value, _protected));
        }
        public void Add(string _name, string _description, bool _value, bool _protected)
        {
            _options.Add(new StarbriteOptionBool(_name, _description, _value, _protected));
        }
        public void Add(string _name, string _description, float _value, bool _protected)
        {
            _options.Add(new StarbriteOptionFloat(_name, _description, _value, _protected));
        }

        // set overload for each type
        public bool Set(string _name, string _value)
        {
            var _option = Get(_name);
            if (_option != null)
            {
                if (_option.Type == "string")
                {
                    (_option as StarbriteOptionString).Value = _value;
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
                    (_option as StarbriteOptionInt).Value = _value;
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
                    (_option as StarbriteOptionBool).Value = _value;
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
                    (_option as StarbriteOptionFloat).Value = _value;
                    return true;
                }
            }
            return false;
        }

        // get methods

    }
}
