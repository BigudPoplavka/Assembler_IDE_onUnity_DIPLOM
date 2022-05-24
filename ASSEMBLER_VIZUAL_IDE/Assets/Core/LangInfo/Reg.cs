using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Reg
{
    private string _bits;
    private (Regname, byte) _wByte0;
    private (Regname, byte) _wByte1;

    public Reg(string bits, (Regname, byte) w0, (Regname, byte) w1)
    {
        _bits = bits;
        _wByte0 = w0;
        _wByte1 = w1;
    }

    public string Bits { get => _bits; set => _bits = value; }
    public (Regname, byte) WByte0 { get => _wByte0; set => _wByte0 = value; }
    public (Regname, byte) WByte1 { get => _wByte1; set => _wByte1 = value; }
}
