using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buider : MonoBehaviour
{
    [SerializeField] private List<Instruction> _instructions;
    [SerializeField] private List<string> _reservedAddreses;
    [SerializeField] private int _instrCount;
    [SerializeField] private string _lastInstructionAddres;
    [SerializeField] private List<Variable> _variables;
    private List<string> _hexLetters = new List<string> { "A", "B", "C", "D", "E", "F" };

    public Buider()
    {
        Instructions = new List<Instruction>();
        _reservedAddreses = new List<string>();
        _variables = Core.lexAnalyzer.variablesTable;

        _lastInstructionAddres = "00";

        GenerateAddrSpace();
    }

    public int InstrCount { get => _instrCount; set => _instrCount = value; }
    public List<Instruction> Instructions { get => _instructions; set => _instructions = value; }

    public void AddInstruction(Instruction instruction)
    {
        Instructions.Add(instruction);

        if (instruction.IsVarDefInstruction)
        {
            Variable var = _variables.Find(x => x.Name == instruction.GetInstructionData(0));
        }

        if(Instructions.Count == _variables.Count)
        {
            CalculateHexVariableAddress();
        }
        
        Debug.Log($"BUILDER: NEW INSTRUCTION ADDED:  {instruction.GetInstructionData()}");
        Debug.Log($"BUILDER: Object Code: {instruction.HexObjectCode} size {instruction.GetCommandSize() }");
    }

    public void AddVarAddrToInstructionsObjectCodes()
    {
        for(int i = _variables.Count; i < Instructions.Count; i++)
        {
            if (Instructions[i].RequireVarAddr)
                AddVarAddrToObjectCode(Instructions[i]);
        }
    }

    private void AddVarAddrToObjectCode(Instruction instruction)
    {
        var varAddr = _variables.Find(x => x.Name == instruction.GetVariableFromInstruction());
        instruction.HexObjectCode += varAddr;
        Debug.Log($"BUILDER: INSTRUCTION UPDATED CODE: {instruction.HexObjectCode}");
    }

    private void CalculateHexVariableAddress()
    {
        _variables.First().Addres = "00";
        string[] tmp = new string[2];

        for(int i = 1; i < _variables.Count; i++)
        {
            tmp[0] = _variables[i - 1].Addres.Substring(0, 1);
            tmp[1] = _variables[i - 1].Addres.Substring(1);

            _variables[i].Addres = GetNextAddres(tmp, _variables[i-1].Size);
        }
    }

    private bool IsNothingToRender()
    {
        if (Instructions.Count == 0)
            return true;
        return false;
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

    private string GetNextAddres(string[] from, int step)
    {
        string[] addr = from;
        string tmp = from[0] + from[1];
        if (step < 16)
        {
            for (int i = 0; i < step; i++)
            {
                addr[1] = IncrementValue(addr[1]);
                if (addr[1] == "0")
                {
                    addr[0] = IncrementValue(addr[0]);
                }
            }

        }
        Debug.Log($"{tmp} + {step} = {addr[0]}{addr[1]}");
        return addr[0] + addr[1];
    }

    private void GenerateAddrSpace()
    {
        string[] addr = new string[] { "0", "F" };
        
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                addr[1] = IncrementValue(addr[1]);
                _reservedAddreses.Add($"{addr[0]}{addr[1]}");             
            }
            addr[0] = IncrementValue(addr[0]);
        }
    }
}
