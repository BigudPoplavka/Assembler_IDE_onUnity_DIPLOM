using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class RegExpressions
{
    // public static Regex db = new Regex();
    public static Regex movByte = new Regex(@"((mov (ah|al|bh|bl|ch|cl|dh|dl) ?, ?((-?\d{1,3})))|(mov (ah|al|bh|bl|ch|cl|dh|dl) ?, ?(ah|al|bh|bl|ch|cl|dh|dl)));?");
    public static Regex movByteAddr = new Regex(@"((mov (ah|al|bh|bl|ch|cl|dh|dl) ?, ?(_?([a-z]+_?([0-9]+)?)_?([a-z]+)?)([0-9]+)?)(([a-z]+)?))\[(ah|al|bh|bl|ch|cl|dh|dl)\];?");
    public static Regex movWord = new Regex(@"((mov (ax|bx|cx|dx) ?, ?((-?\d{1,3})))|(mov (ax|bx|cx|dx) ?, ?(ax|bx|cx|dx)));?");
    public static Regex movWordAddr = new Regex(@"((mov (ax|bx|cx|dx) ?, ?(_?([a-z]+_?([0-9]+)?)_?([a-z]+)?)([0-9]+)?)(([a-z]+)?))\[(ax|bx|cx|dx)\];?");
    public static Regex hexIdentificator = new Regex(@"0?(([A-F])+|([0-9])+)+h");
    public static Regex binIdentificator = new Regex(@"((1|0){8})b");
  

    
}
