using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class RegExpressions
{
    public static Regex emptyLine = new Regex(@" +");
    public static Regex linePartSpaces = new Regex(@"\s+");
    public static Regex comaPattern = new Regex(@"\s*,\s*");
    public static Regex legalChars = new Regex(@"([A-Z]|[a-z])\w+");
    public static Regex movByte = new Regex(@"((mov (ah|al|bh|bl|ch|cl|dh|dl) ?, ?(ah|al|bh|bl|ch|cl|dh|dl)));?");
    public static Regex movByteAddr = new Regex(@"((mov (ah|al|bh|bl|ch|cl|dh|dl) ?, ?))\[(ah|al|bh|bl|ch|cl|dh|dl|ax|bx|cx|dx)\];?");
    public static Regex movWord = new Regex(@"(mov (ax|bx|cx|dx) ?, ?(ax|bx|cx|dx|ah|al|bh|bl|ch|cl|dh|dl));?");
    public static Regex movWordAddr = new Regex(@"((mov (ax|bx|cx|dx) ?, ?(_?([a-z]+_?([0-9]+)?)_?([a-z]+)?)([0-9]+)?)(([a-z]+)?))\[(ax|bx|cx|dx)\];?");
    public static Regex hexIdentificator = new Regex(@"0(([A-F])+|([0-9])+)+h");
    public static Regex binIdentificator = new Regex(@"((1|0){1,8})b");
    public static Regex decIdentificator = new Regex(@"\d");
    public static Regex correctVarName = new Regex(@"(_*([a-z]|[A-Z])\w*_*([0-9]*)?)");
    public static Regex metka = new Regex(@"@?(\w+):");
    
    public static Regex defineVariable = new Regex(@"((_*([a-z]|[A-Z])\w*_*([0-9]*)?) +(db|dw|dd|df|dp|dq|dt)) +");
    public static Regex defineNumOrStrCharArray = new Regex(@"((_*([a-z]|[A-Z])\w*_*([0-9]*)?) +(db|dw|dd|df|dp|dq|dt)) +(((\w+ ?, ?)+\w+)|(\' *(\w* *\w*)*\', ?)+(\' *(\w* *\w*)*\'))");
    public static Regex arrayValues = new Regex(@"(((\w+ ?, ?)+\w+)|(\' *(\w* *\w*)*\', ?)+(\' *(\w* *\w*)*\'))");

    public static Regex segmentDef = new Regex(@"segment (para)? \'code\'");
    public static Regex assumeLine = new Regex(@"(assume *cs:(_*([a-z]|[A-Z])\w*_*([0-9]*)?) *, *ds:(_*([a-z]|[A-Z])\w*_*([0-9]*)?))(( *, *es:)(_*([a-z]|[A-Z])\w*_*([0-9]*)?) *( *, *ss:)(_*([a-z]|[A-Z])\w*_*([0-9]*)?))?");
    public static Regex passPSPblock = new Regex(@"org 100h");
    public static Regex beginJmpMain = new Regex(@"begin: jmp main");
    public static Regex mainProcNearOrFar = new Regex(@"(main proc (near|far))");
    public static Regex mainEndProc = new Regex(@"main endp");
    public static Regex endBegin = new Regex(@"end begin");
    public static Regex endSegment = new Regex(@"ends");
    public static Regex segName = new Regex(@"(cs|es|ds|ss) ?: ?(\w+) ?");

    public static Regex boolValFromJSON = new Regex(@"( (true|false))");
    public static bool IsOnlyRegexMathString(string str)
    {
        if (passPSPblock.IsMatch(str) || beginJmpMain.IsMatch(str)
            || mainEndProc.IsMatch(str) || endBegin.IsMatch(str)
            || mainProcNearOrFar.IsMatch(str))
            return true;
        return false;
    }
}
