using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Register
{
    private Regname _regName;
    private int _size;
    private string _description;

    //private dynamic _value;

    private int _int_val;
    private float _float_val;
    private char _char_val;
    private string _string_val;

    object _minValue, _maxValue;

    private RegValuesRange uRangeByte = new RegValuesRange(0, 255);
    private RegValuesRange RangeByte = new RegValuesRange(-128, 127);
    private RegValuesRange uRangeWord = new RegValuesRange(0, 65535);
    private RegValuesRange RangeWord = new RegValuesRange(-32768, 32767);
    private RegValuesRange uRangeDWord_8086 = new RegValuesRange(-32768, 32767);
    private RegValuesRange RangeDWord_8086 = new RegValuesRange(0, 65535);
    private RegValuesRange uRangeDWord_386 = new RegValuesRange(0, 4294967295);
    private RegValuesRange RangeDWord_386 = new RegValuesRange(-2147483648, 2147483647);

    // dd = df, dp, dq, dt

    private Dictionary<int, RegValuesRange> sizeRangesDict = new Dictionary<int, RegValuesRange>();

    public Register(Regname name, int size, string description)
    {
        RegName = name;
        Size = size;
        Description = description;

        InitDict();
    }

    public void InitDict()
    {
        sizeRangesDict.Add(1, uRangeByte);
        sizeRangesDict.Add(1, RangeByte);
        sizeRangesDict.Add(2, uRangeWord);
        sizeRangesDict.Add(2, RangeWord);
        sizeRangesDict.Add(4, uRangeDWord_8086);
        sizeRangesDict.Add(4, RangeDWord_8086);
        sizeRangesDict.Add(4, uRangeDWord_386);
        sizeRangesDict.Add(4, RangeDWord_386);
    }

    public string Description { get => _description; set => _description = value; }
    public Regname RegName { get => _regName; set => _regName = value; }
    public int Size { get => _size; set => _size = value; }
    //public dynamic Value { get => _value; set => _value = value; }
    public int Int_val { get => _int_val; set => _int_val = value; }
    public char Char_val { get => _char_val; set => _char_val = value; }
    public string String_val { get => _string_val; set => _string_val = value; }
    public float Float_val { get => _float_val; set => _float_val = value; }

    //public void ChangeValue(dynamic value)
    //{
       
    //}

    //public bool IsCorrectValueZise(dynamic value)
    //{
    //    dynamic tmp = value;
        
    //    if(isIntDigit(value) == isString(value) == isChar(value) == false)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        if(value <= sizeRangesDict[_size].Max && value >= sizeRangesDict[_size].Min)
    //        {
    //            return true;
    //        }

           
    //    }
    //    return false;
    //}

    //public bool isIntDigit(dynamic lexemm)
    //{
    //    int tmp = 0;
    //    if (int.TryParse(lexemm, out tmp))
    //    {
    //        Int_val = tmp;
    //        return true;
    //    }
    //    return false;
    //}

    //public bool isFloat(dynamic lexemm)
    //{
    //    float tmp = 0f;
    //    if (float.TryParse(lexemm, out tmp))
    //    {
    //        Float_val = tmp;
    //        return true;
    //    }
    //    return false;
    //}

    //public bool isChar(dynamic lexemm)
    //{
    //    char tmp;
    //    if (char.TryParse(lexemm, out tmp))
    //    {
    //        Char_val = tmp;
    //        return true;
    //    }
    //    return false;
    //}

    //public bool isString(dynamic lexemm)
    //{
    //    string tmp;
    //    object obj = lexemm;
    //    if (obj.GetType() == Type.GetType("string"))
    //    {
    //        String_val = obj.ToString();
    //        return true;
    //    }
    //    return false;
    //}

    // Расширение регистра

    public void Expand(ExpandRegCommands command)
    {

    }
}
