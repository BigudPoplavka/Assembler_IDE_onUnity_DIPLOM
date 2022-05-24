using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

public enum ErrorType
{ 
    UndefinedIdentificator,
    IllegalSymbol,
    MissedComa,
    IllegalOperation, 
    IllegalOperandType,
    IllegalValueSize,
    DifferentOperandSizes,
    IncorrectComTemplate,
    varAlreadyExist,
    CanNotBuild
}
public class Error
{
    private int _line;
    private int _charPos;
    private ErrorType _errorType;
    private string _description;

    public Error(int line, int charPos, ErrorType errorType)
    {
        _line = line;
        _charPos = charPos;
        _errorType = errorType;
    }
    
    public Error(ErrorType errorType, string descr)
    {
        _errorType = errorType;
        _description = descr;
    }

    public int Line { get => _line; set => _line = value; }
    public int CharPos { get => _charPos; set => _charPos = value; }
    public ErrorType ErrorType { get => _errorType; }
    public string Description { get => _description; set => _description = value; }

    public void AppendDescriptionData(string data)
    {
        _description += data;
    }
}