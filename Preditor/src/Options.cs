using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preditor
{
    public class StarbriteOption
    {
        public string Type;
        public string Name;
        public string Description;
        public readonly bool Protected;

        public StarbriteOption(string _type, string _name, bool _protected, string? _description = "")
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
        public StarbriteOptionString(string _name, string _value, bool _protected, string? _description) : base("string", _name, _protected, _description)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }

    public class StarbriteOptionInt : StarbriteOption
    {
        public int Value;
        public readonly int ValueDefault;
        public StarbriteOptionInt(string _name, int _value, bool _protected, string? _description) : base("int", _name, _protected, _description)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }

    public class StarbriteOptionBool : StarbriteOption
    {
        public bool Value;
        public readonly bool ValueDefault;
        public StarbriteOptionBool(string _name, bool _value, bool _protected, string? _description) : base("bool", _name, _protected, _description)
        {
            Value = _value;
            ValueDefault = _value;
        }
    }

    public class StarbriteOptions
    {
        public StarbriteOptions() 
        { 
        }
    }

}
