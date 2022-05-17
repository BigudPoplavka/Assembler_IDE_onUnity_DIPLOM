using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buider : MonoBehaviour
{
    [SerializeField] private List<Instruction> _instructions;
    [SerializeField] private List<string> _reservedAddreses;
    [SerializeField] private int _instrCount;
    [SerializeField] private int _lastInstructionAddres;
    [SerializeField] private Dictionary<Variable, int> _variablesAddreses;
    private List<string> _hexLetters = new List<string> { "A", "B", "C", "D", "E", "F" };

    public Buider()
    {
        _instructions = new List<Instruction>();
        _reservedAddreses = new List<string>();
        _variablesAddreses = new Dictionary<Variable, int>();
    }

    public int InstrCount { get => _instrCount; set => _instrCount = value; }

    public void AddInstruction(Instruction instruction)
    {
        _instructions.Add(instruction);

        if (instruction.IsVarDefInstruction)
        {
            Variable var = Core.lexAnalyzer.variablesTable.Find(x => x.Name == instruction.GetInstructionData(0));
            _variablesAddreses.Add(var, instruction.CommandSize);
        }
          
        Debug.Log($"BUILDER: NEW INSTRUCTION ADDED:  {instruction.GetInstructionData()}");
        Debug.Log($"BUILDER: Object Code: {instruction.HexObjectCode} size {instruction.GetCommandSize() }");
    }
    public void CalculateHexVariableAddress()
    {
        
    }
    public bool IsNothingToRender()
    {
        if (_instructions.Count == 0)
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

    private void GenerateAddrSpace()
    {
        string[] addr = new string[] { "0", "F" };
        
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                addr[1] = IncrementValue(addr[1]);
                _reservedAddreses.Add($"{addr[0]}{addr[1]}");
                //if (_reservedAddreses.Count == _instrCount)
                //    break;
            }
            //if (_reservedAddreses.Count == _instrCount)
            //    break;
            addr[0] = IncrementValue(addr[0]);
        }
    }
}
