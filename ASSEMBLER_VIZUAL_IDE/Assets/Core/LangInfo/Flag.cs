using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class Flag
{
    private RegNameFlags _flagName;
    private int _bitPos;
    private string _description;

    private bool _value;
    private bool _def_value;

    public Flag(RegNameFlags name, int bit, string description, bool def_value)
    {
        FlagName = name;
        BitPos = bit;
        Description = description;
        Def_value = def_value;
    }

    public string Description { get => _description; set => _description = value; }
    public RegNameFlags FlagName { get => _flagName; set => _flagName = value; }
    public int BitPos { get => _bitPos; set => _bitPos = value; }
    public bool Value { get => _value; set => _value = value; }
    public bool Def_value { get => _def_value; set => _def_value = value; }

    public void ChangeValue()
    {
        Value = !Value;
    }

    public void ChangeValue(bool value)
    {
        Value = value;
    }
}

