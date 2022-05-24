using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Instruction
{
    private List<string> _stringParts;
    private OperationTypeTag _operationTypeTag;
    private Dictionary<string, OperandType> _paramsWithType;

    private string _objectCode;
    string reg, sg, d, mod, rm, w, varaddr, tmpVal;
    private List<string> _varDefCommands = new List<string> { "db", "dw", "dd", "dq", "dt" };

    private int commandSize;
    private int currAddr;

    private bool _isOperandRegister;
    private bool _isSecondOperandRegister;
    private bool _isSecondOperandValue;
    private bool _isSecondOperandVar;
    private bool _isAccumulatorReg;
    private bool _isAccumulatorAlReg;
    private bool _isOperandSegReg;
    private bool _isOperandValue;
    private bool _isOperandMemory;
    private bool _isVarDefInstruction;
    private bool _isSecondOperandAccumulator;
    private bool _isSecondOperandAccumulatorAl;
    private bool _requireVarAddr;

    public string HexObjectCode { get => _objectCode; set => _objectCode = value; }
    public bool IsVarDefInstruction { get => _isVarDefInstruction; set => _isVarDefInstruction = value; }
    public int CommandSize { get => commandSize; set => commandSize = value; }
    public bool RequireVarAddr { get => _requireVarAddr;  set => _requireVarAddr = value; }

    public Instruction(List<string> lexems, OperationTypeTag tag, Dictionary<string, OperandType> param)
    {
        _stringParts = lexems;
        _operationTypeTag = tag;
        _paramsWithType = param;

        _isOperandRegister = _paramsWithType.First().Value == OperandType.register;
        _isAccumulatorAlReg = _paramsWithType.First().Key == Regname.al.ToString();
        _isAccumulatorReg = _paramsWithType.First().Key == Regname.ax.ToString();
        _isOperandSegReg = _paramsWithType.First().Value == OperandType.segRegister;
        _isOperandValue = _paramsWithType.First().Value == OperandType.value;
        _isOperandMemory = _paramsWithType.First().Value == OperandType.memory;

        if (param.Count == 2)
        { 
            _isSecondOperandRegister = _paramsWithType.ElementAt(1).Value == OperandType.register;
            _isSecondOperandValue = _paramsWithType.ElementAt(1).Value == OperandType.value;
            _isSecondOperandVar = Core.lexAnalyzer.variablesTable.Any(x => x.Name == _paramsWithType.ElementAt(1).Key);
            _isSecondOperandAccumulator = _paramsWithType.ElementAt(1).Value == OperandType.register
                && Registers.GetRegByName(_paramsWithType.ElementAt(1).Key).RegName == Regname.ax;
            _isSecondOperandAccumulatorAl = _paramsWithType.ElementAt(1).Value == OperandType.register
                && Registers.GetRegByName(_paramsWithType.ElementAt(1).Key).RegName == Regname.al;
        }

        HexObjectCode = GetObjectCode(_operationTypeTag);
        CommandSize = GetCommandSize();
    }

    public Instruction(List<string> lexems, OperationTypeTag tag)
    {
        _stringParts = lexems;
        _operationTypeTag = tag;
        HexObjectCode = GetObjectCode(_operationTypeTag);
        CommandSize = GetCommandSize();
    }

    public string GetVariableFromInstruction()
    {
        foreach (KeyValuePair<string, OperandType> pair in _paramsWithType)
            if (pair.Value == OperandType.memory)
                return pair.Key;
        return string.Empty;
    }

    public string GetInstructionData()
    {
        string paramsStr = string.Empty;

        foreach (string part in _stringParts)
            paramsStr += part + " ";
        return paramsStr;
    }

    public string GetInstructionData(int index)
    {
        if(index >= 0 && index < _stringParts.Count)
            return _stringParts[index];
        return string.Empty;
    }

    public int GetCommandSize()
    {
        for(int i = 0; i < _stringParts.Count; i++)
        {
            if (_varDefCommands.IndexOf(_stringParts[i]) != -1)
                return VariablesSizes.GetSize(_stringParts[i]);
        }
        return _objectCode.Length / 2;
    }

    public string GetObjectCode(OperationTypeTag operationtypetag)
    {
        Debug.Log("********************** GetObjectCode ***********************");
        Debug.Log($"Tag: {operationtypetag}");
        Debug.Log($" _paramsWithType.First()  {_paramsWithType.First().Key} : {_paramsWithType.First().Value}");

        switch (operationtypetag)
        {
            case OperationTypeTag.varDefinition:
                if (_isOperandValue)
                    return ObjectCode.ConvertValueToHex(_paramsWithType.First().Key);
                _isVarDefInstruction = true;
                break;
            case OperationTypeTag.stack:
                if (_stringParts[0] == Commands.pop.ToString())
                {
                    GenerateCodeForPop();
                }
                else if (_stringParts[0] == Commands.push.ToString())
                {
                    if (_isAccumulatorReg)
                        return "50";
                    GenerateCodeForPush();
                }
                break;
            case OperationTypeTag.mathTwoParts:
                if(_stringParts[0] == Commands.add.ToString())
                {
                    return GenerateCodeForAdd();
                }
                else if (_stringParts[0] == Commands.sub.ToString())
                {
                    return GenerateCodeForSub();
                }
                break;
            case OperationTypeTag.movData:
                GenerateCodeForMov();
                break;
        }

        return ObjectCode.ConvertBinToHex(_objectCode); 
    }

    private void GenerateCodeForMov()
    {
        string data = _paramsWithType.ElementAt(1).Key;

        if ((_isOperandRegister || _isOperandMemory) && (_isSecondOperandRegister))
        {
            if(_isOperandRegister)
            {
                w = Registers.GetRegByName(_paramsWithType.First().Key).Size == 1 ? "0" : "1";
                reg = RegTable.regTable[Registers.GetRegByName(_paramsWithType.First().Key)].Bits;
            }
            else if(_isOperandMemory)
            {
                w = Registers.GetRegByName(_paramsWithType.ElementAt(1).Key).Size == 1 ? "0" : "1";
                reg = RegTable.regTable[Registers.GetRegByName(_paramsWithType.ElementAt(1).Key)].Bits;
            }
            d = "1";
            _objectCode = "100010" + d + w + "11" + reg + "110";
        }
        else if (_isSecondOperandValue)
        {
            if (_isOperandRegister)
            {
                if (_isAccumulatorReg)
                {
                    _objectCode = "10100001";
                    RequireVarAddr = true;
                }
                if(_isAccumulatorAlReg)
                {
                    _objectCode = "10100000";
                    RequireVarAddr = true;
                }

                w = Registers.GetRegByName(_paramsWithType.First().Key).Size == 1 ? "0" : "1";
                rm = RegTable.regTable[Registers.GetRegByName(_paramsWithType.First().Key)].Bits;
                _objectCode = "1100011" + w + "11" + "000" + rm;
            }
            else if (_isOperandMemory)
            {
                RequireVarAddr = true;

                if (Core.lexAnalyzer.GetVarSizeByName(_paramsWithType.First().Key) == 1 && _isSecondOperandAccumulatorAl)
                    _objectCode = "10100010";
                else if (Core.lexAnalyzer.GetVarSizeByName(_paramsWithType.First().Key) == 2 && _isSecondOperandAccumulator)
                    _objectCode = "10100011";
                else if(Core.lexAnalyzer.GetVarSizeByName(_paramsWithType.First().Key) == 2)
                {
                    if (_paramsWithType.ElementAt(1).Key == "es")
                        _objectCode = "8C06";
                    if (_paramsWithType.ElementAt(1).Key == "cs")
                        _objectCode = "8C0E";
                }

                w = Core.lexAnalyzer.GetVarSizeByName(_paramsWithType.ElementAt(1).Key) == 1 ? "0" : "1";
                rm = RegTable.regTable[Registers.GetRegByName(_paramsWithType.First().Key)].Bits;
                _objectCode = "1100011" + w + "00" + "000" + rm; 
            }

            _objectCode += ObjectCode.ConvertValueToHex(data);
        }
        else if ((_isAccumulatorReg || _isAccumulatorAlReg) && _isSecondOperandVar)
        {
            w = Registers.GetRegByName(_paramsWithType.First().Key).Size == 1 ? "0" : "1";
            _objectCode = "1010000" + w;
            RequireVarAddr = true;
        }
    }

    private void GenerateCodeForPop()
    {
        if (_isOperandRegister)
            _objectCode = "01011" + RegTable.regTable[Registers.GetRegByName(_paramsWithType.First().Key)].Bits;
        else if (_isOperandMemory)
        {
            _objectCode = "10001111" + "00" + "000" + "110";  // + var addr!
            RequireVarAddr = true;
        }
        else if (_isOperandSegReg)
            _objectCode = "000" + SgTable.sg[_paramsWithType.First().Key] + "111";
    }

    private void GenerateCodeForPush()
    {
        if (_isOperandRegister)
            _objectCode = "01010" + RegTable.regTable[Registers.GetRegByName(_paramsWithType.First().Key)];
        else if (_isOperandSegReg)
            _objectCode = "000" + SgTable.sg[_paramsWithType.First().Key] + "111";
        else if (_isOperandMemory)
            _objectCode = "11111111" + "00" + "110" + "110";
    }

    private void GenerateWByte(string regName)
    {
        if (Registers.GetRegByName(regName).Size == 1)
            w = "0";
        else if (Registers.GetRegByName(regName).Size == 2)
            w = "1";
    }

    private string GenerateCodeForAdd()
    {
        string regName;
        if (_isOperandRegister)
        {
            Debug.Log($"First param reg {_paramsWithType.First().Key}");
            regName = _paramsWithType.First().Key;
            reg = RegTable.regTable[Registers.GetRegByName(regName)].Bits;
            GenerateWByte(regName);
        }
        if (_isSecondOperandRegister)
        {
            Debug.Log($"Second param reg {_paramsWithType.ElementAt(1).Key}");
            regName = _paramsWithType.ElementAt(1).Key;
            rm = RegTable.regTable[Registers.GetRegByName(regName)].Bits;
            GenerateWByte(regName);
        }

        if((_isAccumulatorReg || _isAccumulatorAlReg) && _isSecondOperandValue)
        {
            _objectCode = ObjectCode.ConvertBinToHex("10" + w);
            _objectCode += ObjectCode.ConvertValueToHex(_paramsWithType.ElementAt(1).Key);
        }
        else if(_isOperandRegister && _isSecondOperandRegister)
        {
            _objectCode = ObjectCode.ConvertBinToHex("1" + w);
            _objectCode += ObjectCode.ConvertValueToHex("11" + reg + rm);
        }
        else if(_isOperandRegister && _isSecondOperandVar)
        {
            _objectCode = ObjectCode.ConvertBinToHex("1" + w);
            _objectCode += ObjectCode.ConvertValueToHex("00" + reg + "110");
        }
        return _objectCode;
    }

    private string GenerateCodeForSub()
    {
        string regName;
        if (_isOperandRegister)
        {
            Debug.Log($"First param reg {_paramsWithType.First().Key}");
            regName = _paramsWithType.First().Key;
            reg = RegTable.regTable[Registers.GetRegByName(regName)].Bits;
            GenerateWByte(regName);
        }
        if (_isSecondOperandRegister)
        {
            Debug.Log($"Second param reg {_paramsWithType.ElementAt(1).Key}");
            regName = _paramsWithType.ElementAt(1).Key;
            rm = RegTable.regTable[Registers.GetRegByName(regName)].Bits;
            GenerateWByte(regName);
        }

        if ((_isAccumulatorReg || _isAccumulatorAlReg) && _isSecondOperandValue)
        {
            _objectCode = ObjectCode.ConvertBinToHex("10110" + w);
            _objectCode += ObjectCode.ConvertValueToHex(_paramsWithType.ElementAt(1).Key);
        }
        else if (_isOperandRegister && _isSecondOperandRegister)
        {
            _objectCode = ObjectCode.ConvertBinToHex("1010" + "1" + w);
            _objectCode += ObjectCode.ConvertValueToHex("11" + reg + rm);
        }
        else if(_isOperandRegister && _isSecondOperandValue)
        {
            _objectCode = ObjectCode.ConvertBinToHex("1010" + "1" + w);
            _objectCode += ObjectCode.ConvertValueToHex("00" + reg + "110");
        }
        return _objectCode;
    }
}
