using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ProcType
{ 
    i8086, i386
}

public enum SignTypeFlag
{
    u, s
}

public enum RegNameFlags
{
    ID, VIP, VIF, AC, VM, RF, NT, IOPL, OF, DF, IF, TF, SF, ZF, AF, PF, CF
}

public enum Regname
{
    al, ah, bl, bh, cl, ch, dl, dh, ax, bx, cx, dx, eax, ebx, ecx, edx, CS, DS, SS, ES, si, di, sp, bp, ip
}

public enum Commands
{
    db, dw, dd, df, dp, dq, dt, dup, 
    mov, push, pop, 
    add, sub, idiv, imul, inc, dec,
    cmp, equ,
    and, or, eor, xor, not,
    je, jne, jl, jnge, jle, jng, jg, jnle, jge, jnl, jb, jnae, jbe, jna, ja, jnbe, jae, jnb,
    loop, loope, loopz, loopne, loopnz,
    lods, lodsb, lodsw, lodsd,
    test, bound,
    macro, endm, ret
}

public enum ExpandRegCommands
{
    cbw, cwd, cwde, cdq
}

public enum AloneLexemms
{

}

public enum OneOrMoreParamsCommands
{
    push, pop
}

public enum IdentificatorType
{ 
    String, Digit, Binary, Hex, Char, Arr, Null, Unknown
}

public enum DoubleWordOperation
{
    jumps,
    je, jne, jl, jnge, jle, jng, jg, jnle, 
    jge, jnl, jb, jnae, jbe, jna, ja, jnbe, jae, jnb,
    imul, idiv, Int
}

public static class PairWordsString
{
    public static List<(string, int)> pairCommands = new List<(string, int)> {
        ("je", 1), ("jne", 1), ("jl", 1), ("jnge", 1), ("jle", 1), ("jng", 1), ("jg", 1), ("jnle", 1),
        ("jge", 1), ("jnl", 1), ("jb", 1), ("jnae", 1), ("jbe", 1), ("jna", 1), ("ja", 1), ("jnbe", 1),( "jae", 1), ("jnb", 1),
         ("imul", 1),( "idiv", 1),( "int", 1)
    };

    public static bool IsDoubleWordsCommand(string str)
    {
        if(pairCommands.Any(u => u.Item1 == str))
            return true;

        return false;
    }

    public static bool IsCorrectDoubleLexPosition(string str, int pos)
    {
        foreach((string, int) pair in pairCommands)
            if (pair.Item1 == str && pair.Item2 == pos)
                return true;

        return false;
    }

    public static bool IsCorrectCommand(string[] str)
    {
        if(IsDoubleWordsCommand(str[0]) && IsCorrectDoubleLexPosition(str[0], 1))
            return true;

        return false;
    }
}

public static class EnumTypesBase
{
    public static List<string> GetCommandsArray()
    {
        List<string> commands = new List<string>();

        foreach (Commands command in Enum.GetValues(typeof(Commands)))
            commands.Add(command.ToString());
        return commands;
    }
}

public static class Operators
{ 
    
}


