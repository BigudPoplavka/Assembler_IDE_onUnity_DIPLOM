using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectCode
{
    private static Dictionary<string, string> hexBinDict = new Dictionary<string, string>()
    {
        { "0001", "1" }, { "0010", "2" }, { "0011", "3" }, { "0100", "4" }, { "0101", "5" }, { "0110", "6" },
        { "0111", "7" }, { "1000", "8" }, { "1001", "9" }, { "1010", "A" }, { "1011", "B" }, { "1100", "C" },
        { "1101", "D" }, { "1110", "E" }, { "1111", "F" }, { "0000", "" }
    };
    public static string ConvertBinToHex(string value)  
    {
        Debug.Log("************ ConvertBinToHex ****************");
        Debug.Log($" Value {value}");
        if (value.Length == 4)
            return hexBinDict[value];
        
        string hexVal = string.Empty;
        string tmpBits = string.Empty;

        if (value.Length < 4)
        {
            tmpBits = new string('0', (4 - value.Length)) + value;
            Debug.Log($"hexBinDict {tmpBits}");
            return hexBinDict[tmpBits];
        }
        if(value.Length > 4 && value.Length <= 8)   // 0000**** учесть нули
        {
            tmpBits = new string('0', (8 - value.Length)) + value;
            Debug.Log($"hexBinDict  {hexBinDict[tmpBits.Substring(0, 4)] + hexBinDict[tmpBits.Substring(4, 4)]}");
            return hexBinDict[tmpBits.Substring(0, 4)] + hexBinDict[tmpBits.Substring(4, 4)];
        }
        return hexVal;
    }
    public static string ConvertValueToHex(string value)
    {
        string tmp = string.Empty;
        switch (Core.lexAnalyzer.DefineIdentificatorType(value))
        {
            case IdentificatorType.Digit:
                return Convert.ToString(int.Parse(value), 16).ToUpper();
            case IdentificatorType.Hex:
                Debug.Log($"ConvertValueToHex {value}");
                if (value.Length == 3)
                    tmp = value.Substring(0, value.Length - 1);
                else 
                    tmp = value.Substring(1, value.Length - 2);
                return tmp.ToUpper();
            case IdentificatorType.Binary:
                tmp = value.Substring(0, value.Length - 1);
                return ConvertBinToHex(tmp);
        }
        return "Err";
    }
}
