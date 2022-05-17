using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Variable
{
    private string _name;
    private string _lexValue;
    private string _definitionSize;
    private int _defineStringIndex;
    private int _size;
    private string _addres;

    private List<string> _array;

    public Variable(string name, int stringIndex, string defSize)
    {
        Name = name;
        DefineStringIndex = stringIndex;
        DefinitionSize = defSize;
       
    }

    public Variable(string name, int stringIndex, string defSize, string lex)
    {
        Name = name;
        DefineStringIndex = stringIndex;
        DefinitionSize = defSize;
        LexValue = lex;

        Size = VariablesSizes.GetSize(defSize);

        GlobalVariables._variablesCount++;
    }

    public string Name { get => _name; set => _name = value; }
    public string LexValue { get => _lexValue; set => _lexValue = value; }
    public string DefinitionSize { get => _definitionSize; set => _definitionSize = value; }
    public int DefineStringIndex { get => _defineStringIndex; set => _defineStringIndex = value; }
    public int Size { get => _size; set => _size = value; }
    public string Addres { get => _addres; set => _addres = value; }

    public string GetVariableData()
    {
        return $"Line {DefineStringIndex}, name {Name}, def command {DefinitionSize}, size {Size}, value {LexValue}";
    }
}
