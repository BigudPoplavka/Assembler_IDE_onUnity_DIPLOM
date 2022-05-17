using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HexAddres
{
    private string _hexAddres;
    private string _calculateblePart = "00";
    private string _zero = "0";
    private List<string> _hexLetters = new List<string> { "A", "B", "C", "D", "E", "F" };

    public HexAddres(bool isFirst)
    {
        if (isFirst)
            _hexAddres = "0000";
    }

    public void CalculateAddres(int pastInstrSize)
    {
        
        for(int i = 0; i < pastInstrSize; i++)
        {
            
        }
    }

    private string IncrementValue(string value)
    {
        if (value == "9")
            return _hexLetters.First();
        else if (_hexLetters.Contains(value))
        {
            int nextIndex = _hexLetters.IndexOf(value) + 1;
            return value == _hexLetters.Last() ? "0" : _hexLetters[nextIndex];
        }
        int tmp = int.Parse(value);
        tmp++;
        return tmp++.ToString();
    }

}
