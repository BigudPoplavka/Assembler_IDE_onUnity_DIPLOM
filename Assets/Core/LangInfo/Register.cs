using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Register
{
    private Regname _regName;
    private int _size;
    private int _int_val;

    private float _float_val;

    private char _char_val;

    private string _string_val;
    private string _description;

    object _minValue, _maxValue;

    public RegValuesRange uRangeByte = new RegValuesRange(0, 255);
    public RegValuesRange RangeByte = new RegValuesRange(-128, 127);
    public RegValuesRange uRangeWord = new RegValuesRange(0, 65535);
    public RegValuesRange RangeWord = new RegValuesRange(-32768, 32767);
    public RegValuesRange uRangeDQ = new RegValuesRange(-9223372036854775808, 9223372036854775807);
    private RegValuesRange err = new RegValuesRange(0,0);

    public Dictionary<RegValuesRange, int> sizeRangesDict = new Dictionary<RegValuesRange, int>();

    public Register(Regname name, int size, string description)
    {
        RegName = name;
        Size = size;
        Description = description;

        InitDict();
    }

    public void InitDict()
    {
        sizeRangesDict.Add(uRangeByte, 1);
        sizeRangesDict.Add(RangeByte, 1);
        sizeRangesDict.Add(uRangeWord, 2);
        sizeRangesDict.Add(RangeWord, 2);
        sizeRangesDict.Add(uRangeDQ, 8);
    }

    public string Description { get => _description; set => _description = value; }
    public Regname RegName { get => _regName; set => _regName = value; }
    public int Size { get => _size; set => _size = value; }
    public int Int_val { get => _int_val; set => _int_val = value; }
    public char Char_val { get => _char_val; set => _char_val = value; }
    public string String_val { get => _string_val; set => _string_val = value; }
    public float Float_val { get => _float_val; set => _float_val = value; }

    public RegValuesRange GetRange(int size)
    {
        foreach(KeyValuePair<RegValuesRange, int> pair in sizeRangesDict)
            if (pair.Value == size)
                return pair.Key;
        return err;
    }

    public void Expand(ExpandRegCommands command)
    {

    }
}
