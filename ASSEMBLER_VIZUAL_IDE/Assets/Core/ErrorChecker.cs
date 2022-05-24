using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;


[System.Serializable]
public class NewErrorFinded : UnityEvent<int> { }

[System.Serializable]
public class NoErrors : UnityEvent { }
public class ErrorChecker : MonoBehaviour
{
    [SerializeField] public NewErrorFinded newError = new NewErrorFinded();
    [SerializeField] public NoErrors noErrors = new NoErrors();
    [SerializeField] private int _errorsCount;
    [SerializeField] private List<Error> _errors;
    [SerializeField] private List<string> _currAnalyzingString;
    [SerializeField] List<string> paramsList;

    private OperationTypeTag _currOperationTag;

    private int _dWorduMin = 0;
    private int _dWordMin = -2147483648;
    private int _dWordMax = 2147483647;
    private long _dWorduMax = 4294967295;

    private Dictionary<string, OperandType> _operandsAndTypes;
    private Dictionary<List<string>, OperationTypeTag> _commandsAndTypes;
    public int ErrorsCount { get => _errors.Count; }
    public ErrorChecker() 
    {
        _errors = new List<Error>();
        _currAnalyzingString = new List<string>(); 
        _errorsCount = _errors.Count;
        _operandsAndTypes = new Dictionary<string, OperandType>();

        _commandsAndTypes = new Dictionary<List<string>, OperationTypeTag>()
        {
            { new List<string>() { "db", "dw", "dd", "df", "dp", "dq", "dt", "dup"}, OperationTypeTag.varDefinition },
            { new List<string>() { "mov" }, OperationTypeTag.movData },
            { new List<string>() {  "je", "jne", "jl", "jnge", "jle", "jng",
                "jg", "jnle", "jge", "jnl", "jb", "jnae", "jbe", "jna", "ja", "jnbe", "jae", "jnb"  
            }, OperationTypeTag.jumps },
            { new List<string>() { "add", "sub", }, OperationTypeTag.mathTwoParts },
            { new List<string>() { "inc", "dec", "mul", "div", "imul", "idiv"}, OperationTypeTag.mathOneOperand },
            { new List<string>() { "cmp", "equ" }, OperationTypeTag.cmp },
            { new List<string>() { "push", "pop" }, OperationTypeTag.stack },
            { new List<string>() { "ret", "cbw", "cwd", "cdq" }, OperationTypeTag.oneCommandOperation }
        };
    }

    public Error AddErrDescription(string logStr, string appendedData, Error error)
    {
        string tmp = logStr;
        Error tmpErr = error;
        tmp += " " + appendedData;
        tmpErr.Description = tmp;

        return tmpErr;
    }

    public void AddError(Error error)
    {
        _errors.Add(error);
        Core.statusBarRenderer.ShowErrors(_errors.Count);
        Debug.Log($"ERROR CHECKER: ERROR ADDED!!! Count: {_errors.Count} " + error.Description);
        Core.ShowLog(error);
    }

    public void ClearError(Error error)
    {
        Debug.Log($"Error to remove -----> {error.Description}");
        if(IsErrorListContains(error))
        {
            Debug.Log("CONTAINS");
            _errors.Remove(_errors.Find(x => x.Description == error.Description));
            Debug.Log($"Err list len {_errors.Count}");
            if (_errors.Count == 0)
            {
                Core.statusBarRenderer.ShowNoErrors();
            }
        }
    }

    public void ClearCurrOperandsDict()
    {
        _operandsAndTypes.Clear();
    }

    public bool IsErrorListContains(Error error)
    {
        if (_errors.Contains(error))
            return true;
        return false;
    }

    public void ClearErrors()
    {
        _errors.Clear();
        Core.statusBarRenderer.ShowNoErrors();
    }

    public bool IsRegister(string lex)
    {
        foreach (Regname regname in Enum.GetValues(typeof(Regname)))
            if (lex == regname.ToString())
                return true;
        return false;
    }

    public bool IsSegment(string lex)
    {
        foreach (Segments regname in Enum.GetValues(typeof(Segments)))
            if (lex == regname.ToString())
                return true;
        return false;
    }

    public bool IsMemory(string lex)
    {
        foreach (Variable variable in Core.lexAnalyzer.variablesTable)
            if (variable.Name == lex)
                return true;
        return false;
    }

    public bool IsValue(string lex)
    {
        if (Core.lexAnalyzer.isIdentificator(lex) &&
            Core.lexAnalyzer.DefineIdentificatorType(lex) != IdentificatorType.Var)
            return true;
        return false;
    }

    public bool IsCorrectValRange(string lex)
    {
        Register tmp = Registers.GetRegByName(lex);
        switch (Core.lexAnalyzer.DefineIdentificatorType(lex))
        {
            case IdentificatorType.Digit:
                if (IsCorrectDigitValSize(int.Parse(lex), tmp))
                    return true;                   
                return false;
            case IdentificatorType.Hex:
                if (IsCorrectHexSize(lex, tmp))
                    return true;
                return false;
            case IdentificatorType.Binary:
                if (IsCorrectBinSize(lex, tmp))
                    return true;
                return false;

        }
        return false;
    }

    public bool IsCorrectDigitValSize(int val, Register register, int extraSize = 0)
    {
        if (val < 0)
        {
            switch(extraSize)
            {
                case 0:
                    break;
                case 4:
                    return (val >= _dWordMin && val <= _dWordMax);
                case 8:
                    return (val >= register.uRangeDQ.Min && val <= register.uRangeDQ.Max);
            }
            switch (register.Size)
            {
                case 1:
                    return (val >= register.RangeByte.Min && val <= register.RangeByte.Max);
                case 2:
                    return (val >= register.RangeWord.Min && val <= register.RangeWord.Max);
            }
        }
        else if(val >= 0)
        {
            switch (extraSize)
            {
                case 0:
                    break;
                case 4:           
                    return (val >= _dWorduMin && val <= _dWorduMax);
                case 8:
                    return (val >= register.uRangeDQ.Min && val <= register.uRangeDQ.Max);
            }
            switch (register.Size)
            {
                case 1:
                    return (val >= register.uRangeByte.Min && val <= register.uRangeByte.Max);
                case 2:
                    return (val >= register.uRangeWord.Min && val <= register.uRangeWord.Max);
            }
        }
        return false;
    }

    public bool IsCorrectLongSize(long val, Register register)
    {
        return (val >= register.uRangeDQ.Min && val <= register.uRangeDQ.Max);
    }

    public bool IsCorrectHexSize(string val, Register register, int extraSize = 0)
    {
        string tmp = string.Empty;
        if (val.EndsWith("h") && val.StartsWith("0"))
        {
            tmp = val.Length == 3 ? val.Substring(0, val.Length - 1) : val.Substring(1, val.Length - 2);
        }
           
        if(extraSize != 0)
        {
            switch (extraSize)
            {
                case 0:
                    break;
                case 4:
                    return tmp.Length <= 8;
                case 8:
                    return tmp.Length <= 16;
            }
        }

        Debug.Log($"IsCorrectHexSize: {tmp} reg.size {register.Size} | val length {tmp.Length}");
        if(register.Size == 1) // || register.Size == 2
            return (tmp.Length == 2);
        else
            return (tmp.Length == register.Size * 2);
    }

    public bool IsCorrectBinSize(string val, Register register)
    {
        string tmp = string.Empty;
        int[] smallBinValueLength = new int[] { 1, 2, 3};
        if (val.EndsWith("b"))
            tmp = val.Substring(0, val.Length - 1);
        Debug.Log($"IsCorrectBinSize ----- {tmp}");

        if (smallBinValueLength.Any(x => x == tmp.Length))
            tmp = new string('0', (4 - tmp.Length)) + tmp;
        if (val.Length > 4 && val.Length < 8)
            tmp = new string('0', (8 - tmp.Length)) + tmp;

        Debug.Log($"IsCorrectBinSize ----- {tmp}, {register.Size}");
        if (register.Size == 1)
            return (tmp.Length == 4 || tmp.Length == 8);
        else
            return (tmp.Length == register.Size * 8);
    }

    public bool IsCorrectMovOperation(string param_1, string param_2)
    {
        Debug.Log("*********************** IsCorrectMovOperation *******************************");
        Debug.Log($"PARAMS: param_1 {param_1}, param_2 {param_2}");
        
        // to reg 
        if (IsRegister(param_1))
        {
            Register currReg = Registers.GetRegByName(param_1);
            if(IsRegister(param_2))   // reg to reg
            {
                if (Registers.IsSameSizeRegisters(param_1, param_2))
                {
                    Debug.Log($"Same size {param_1}, {param_2}: {Registers.IsSameSizeRegisters(param_1, param_2)}");
                    Debug.Log($"New MOV REG, REG: {param_1}, {param_2}");
                    return true;
                }
                else
                {
                    AddError(new Error(ErrorType.DifferentOperandSizes, $"ERROR!!! {param_1} size is {currReg.Size} bytes," +
                        $" but '{param_2}' size {Registers.GetRegByName(param_2).Size} bytes"));
                    Core.statusBarRenderer.ShowSolveText($"Try to mov '{param_2}' to register with size {Core.lexAnalyzer.GetVarSizeByName(param_2)} " +
                        $"bytes: ({Registers.GetRegsWithSize(Core.lexAnalyzer.GetVarSizeByName(param_2))})");
                }
            }
            else if (IsMemory(param_2))   // memory to reg
            {
                if (currReg.Size == Core.lexAnalyzer.GetVarSizeByName(param_2))
                    return true;
                AddError(new Error(ErrorType.DifferentOperandSizes, $"ERROR!!! {param_1} size is {currReg.Size} bytes, but '{param_2}' declared size {Core.lexAnalyzer.GetVarSizeByName(param_2)} bytes"));
                Core.statusBarRenderer.ShowSolveText($"Try to mov variable '{param_2}' to register with size {Core.lexAnalyzer.GetVarSizeByName(param_2)} " +
                    $"bytes: ({Registers.GetRegsWithSize(Core.lexAnalyzer.GetVarSizeByName(param_2))}) or declare '{param_2}' with size {currReg.Size} bytes.");
                return false;
            }
            else if(IsValue(param_2))   // value to reg
            {
                switch(Core.lexAnalyzer.DefineIdentificatorType(param_2))
                {
                    case IdentificatorType.Digit:
                        if (IsCorrectDigitValSize(int.Parse(param_2), currReg))
                            return true;
                        else
                        {
                            AddError(new Error(ErrorType.IllegalValueSize, LogMessagesBase.LogMessageErrRange(param_1, param_2, currReg)));
                            return false;
                        }
                    case IdentificatorType.Hex:
                        if (IsCorrectHexSize(param_2, currReg))
                            return true;
                        else
                        {
                            AddError(new Error(ErrorType.IllegalValueSize, LogMessagesBase.LogMessageErrRange(param_1, param_2, currReg)));
                            return false;
                        }
                    case IdentificatorType.Binary:
                        if (IsCorrectBinSize(param_2, currReg))
                            return true;
                        else
                        {
                            AddError(new Error(ErrorType.IllegalValueSize, LogMessagesBase.LogMessageErrRange(param_1, param_2, currReg)));
                            return false;
                        }
                }
            }
        }
        else if(IsMemory(param_1))  // to mem 
        {
            if(IsRegister(param_2))
            {
                var variable = Core.lexAnalyzer.variablesTable.Find(x => x.Name == param_1);
                int size = Registers.GetRegByName(param_2).Size;
                return variable.Size == size;
            }

            //Variable tmp = Core.lexAnalyzer.variablesTable.Find(x => x.Name == param_1);
            //return IsCorrectVarDef(tmp.DefinitionSize, param_2);
        }
        return false;
    }

    public bool IsCorrectVarDef(string param_1, string param_2)
    {
        int currDefSize = VariablesSizes.GetSize(param_1);
        Debug.Log($"IsCorrectVarDef -- size {currDefSize}-- {param_2} type {Core.lexAnalyzer.DefineIdentificatorType(param_2)}");

        switch (Core.lexAnalyzer.DefineIdentificatorType(param_2))
        {
            case IdentificatorType.Digit:
                if(currDefSize == 4)
                {
                    if (IsCorrectDigitValSize(int.Parse(param_2), null, 4))
                        return true;   
                }
                                                   
                else if (IsCorrectDigitValSize(int.Parse(param_2), Registers.GetRegBySize(currDefSize)))
                    return true;
                break;
            case IdentificatorType.Hex:
                if (currDefSize == 4)
                    if (IsCorrectHexSize(param_2, null, 4))
                        return true;
                if (IsCorrectHexSize(param_2, Registers.GetRegBySize(currDefSize)))
                    return true;
                break;
            case IdentificatorType.Binary:
                if (IsCorrectBinSize(param_2, Registers.GetRegBySize(currDefSize)))
                    return true;
                break;
        }
        AddError(new Error(ErrorType.IllegalValueSize, LogMessagesBase.LogMessageErrRange(param_1, param_2, Registers.GetRegBySize(currDefSize))));
        return false;
    }

    public bool IsCorrectMathTwoOperands(string param_1, string param_2)
    {
        if (IsRegister(param_1) && IsRegister(param_2))
        {
            if (Registers.IsSameSizeRegisters(param_1, param_2))
                return true;
            else
            {
                AddError(new Error(ErrorType.DifferentOperandSizes, $"ERROR!!! {param_1} size is {Registers.GetRegByName(param_1).Size} bytes, " +
                    $"but '{param_2}' size is {Registers.GetRegByName(param_2).Size} bytes"));
                Core.statusBarRenderer.ShowSolveText($"Try add to '{param_1}' register with the same size {Registers.GetRegsWithSize(Core.lexAnalyzer.GetVarSizeByName(param_2))} ");
                return false;
            }
        }
        else if((IsRegister(param_1) && IsMemory(param_2)) || (IsRegister(param_2) && IsMemory(param_1)))
        {
            if(Registers.GetRegByName(param_1).Size == Core.lexAnalyzer.GetVarSizeByName(param_2))
                return true;
            else
            {
                if(IsRegister(param_1))
                {
                    AddError(new Error(ErrorType.DifferentOperandSizes, $"ERROR!!! {param_1} size is {Registers.GetRegByName(param_1).Size} bytes, " +
                    $"but '{param_2}' size is {Core.lexAnalyzer.GetVarSizeByName(param_2)} bytes"));
                    Core.statusBarRenderer.ShowSolveText($"Try add/sub '{param_1}' and variable with the same size {Core.lexAnalyzer.GetVarSizeByName(param_2)} ");
                }
                if (IsRegister(param_2))
                {
                    AddError(new Error(ErrorType.DifferentOperandSizes, $"ERROR!!! {param_1} size is {Core.lexAnalyzer.GetVarSizeByName(param_1)} bytes, " +
                    $"but '{param_2}' size is {Registers.GetRegByName(param_2).Size} bytes"));
                    Core.statusBarRenderer.ShowSolveText($"Try add/sub '{param_1}' and register with the same size {Registers.GetRegByName(param_2).Size} ");
                }
                return false;
            }
        }
        else if((IsRegister(param_1) && IsValue(param_2)) || IsMemory(param_1) && IsValue(param_2))
        {
            Register tmp = Registers.GetRegByName(param_1);
            int firstOperandSize;
            firstOperandSize = IsRegister(param_1) ? Registers.GetRegByName(param_1).Size : Core.lexAnalyzer.GetVarSizeByName(param_1);

            if(IsCorrectValRange(param_2))
            {
                return true;
            }
            else
            {
                AddError(new Error(ErrorType.IllegalValueSize, LogMessagesBase.LogMessageErrRange(param_1, param_2, tmp)));
                return false;
            }
        }
        else if(IsMemory(param_1) && IsMemory(param_2))
        {
            AddError(new Error(ErrorType.IllegalOperandType, "ERROR!!! Operations 'add'/'sub' can't take two memory-opernds at the same time!"));
            Core.statusBarRenderer.ShowSolveText($"Try add to mov one of operands to register and then make add/sub [reg,mem], [reg,reg], [reg,val], [mem,val]");
            return false;
        }
        return false;
    }

    public bool IsCorrectStackOperation(List<string> command)
    {
        if (IsRegister(command[1]))
            return (Registers.GetRegByName(command[1]).Size != 1);
        if (IsMemory(command[1]))
            return (Core.lexAnalyzer.GetVarSizeByName(command[1]) != 1);

        if (command[0] == Commands.pop.ToString())
            if(IsRegister(command[1]))
                return command[1] != Registers.CS.RegName.ToString();
        return false;
    }

    public bool IsCorrectJump(string param_1, string param_2)
    {
        if (Core.lexAnalyzer.marks.Contains(param_2))
            return true;
        AddError(new Error(ErrorType.IllegalOperation, LogMessagesBase.LogMessageErrMark(param_2)));
        return false;
    }

    public void SaveParamsAndTypes(List<string> line)
    {
        Debug.Log("*********************** SaveParamsAndTypes *******************************");
        for (int i = 1; i < line.Count; i++)
        {
            if (IsRegister(line[i]))
            {
                if (IsSegment(line[i]))
                    _operandsAndTypes.Add(line[i], OperandType.segRegister);
                else
                {
                    Debug.Log($"{i}-th param is Register: {line[i]}");
                    _operandsAndTypes.Add(line[i], OperandType.register);
                }
            }
            else if (IsMemory(line[i]))
            {
                if (Core.lexAnalyzer.DefineIdentificatorType(line[i]) == IdentificatorType.Var)
                {
                    if (!Core.lexAnalyzer.variablesTable.Any(x => x.Name == line[i]))
                    {
                        Debug.Log($"ERROR! The variable '{line[i]}' has not been declared before!");
                        AddError(Core.lexAnalyzer.undefIdentificator);
                    }
                    else
                    {
                        Debug.Log($"{i}-th param is Memory: {line[i]}");
                        _operandsAndTypes.Add(line[i], OperandType.memory);
                    }
                }
            }
            else if (Core.lexAnalyzer.isIdentificator(line[i]))
            {              
                Debug.Log($"{i}-th param is Value: {line[i]}");
                _operandsAndTypes.Add(line[i], OperandType.value);                         
            }
        }
    }

    public OperationTypeTag GetOperationTypeTag(string command)
    {
        foreach (KeyValuePair<List<string>, OperationTypeTag> pair in _commandsAndTypes)
            if (pair.Key.Contains(command))
                return pair.Value;
        return OperationTypeTag.unkn;
    }

    public (bool, OperationTypeTag, Dictionary<string, OperandType>) IsCorrectOperation(string[] line)
    {
        Debug.Log("*********************** IsCorrectOperation *******************************");

        _operandsAndTypes.Clear();

        string tmp = string.Empty;
        string param = string.Empty;
        foreach (string el in line)
        { 
            tmp += el;
            tmp += ' ';
        }
        Debug.Log(tmp);

        _currAnalyzingString = line.ToList();
        
        if(EnumTypesBase.GetCommandsArray().Contains(_currAnalyzingString[0]))
            _currOperationTag = GetOperationTypeTag(_currAnalyzingString[0]);
        else if(EnumTypesBase.GetCommandsArray().Contains(_currAnalyzingString[1]))
            _currOperationTag = GetOperationTypeTag(_currAnalyzingString[1]);

        SaveParamsAndTypes(_currAnalyzingString); 

        switch(_currOperationTag)
        {
            case OperationTypeTag.movData:
                if (IsCorrectMovOperation(line[1], line[3]))  
                    return (true, _currOperationTag, _operandsAndTypes);
                break;
            case OperationTypeTag.varDefinition:
                if (IsCorrectVarDef(line[1], line[2]))
                    return (true, _currOperationTag, _operandsAndTypes);
                break;
            case OperationTypeTag.mathTwoParts:
                if (IsCorrectMathTwoOperands(line[1], line[3]))
                    return (true, _currOperationTag, _operandsAndTypes);
                break;
            case OperationTypeTag.stack:
                if (IsCorrectStackOperation(_currAnalyzingString))
                    return (true, _currOperationTag, _operandsAndTypes);
                break;
            case OperationTypeTag.oneCommandOperation:
                Debug.Log($"ONE COMMAND OPERATION! {_currAnalyzingString[0]}");
                break;
            case OperationTypeTag.jumps:
                if(IsCorrectJump(line[0], line[1]))
                    return (true, _currOperationTag, _operandsAndTypes);
                break;
        }

        return (false, OperationTypeTag.unkn, null);
    }

}

