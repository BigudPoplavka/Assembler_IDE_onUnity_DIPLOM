﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RegValuesRange
{
    private int _min;
    private int _max;

    private uint _umin;
    private uint _umax;

    public RegValuesRange(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public RegValuesRange(uint umin, uint umax)
    {
        _umin = umin;
        _umax = umax;
    }

    public bool isCorrectDigitValue(int value)
    {
        return (value >= _min && value <= _max) ? true : false;
    }

    public bool isCorrectDigitUValue(uint value)
    {
        return (value >= _min && value <= _max) ? true : false;
    }

    public int Min { get => _min; set => _min = value; }
    public int Max { get => _max; set => _max = value; }
}
