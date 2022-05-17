using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum ProcType
{ 
    i8086, i386
}

public enum ProgramType
{ 
    com, exe
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
    al, ah, bl, bh, cl, ch, dl, dh, ax, bx, cx, dx, eax, ebx, ecx, edx, cs, ds, ss, es, si, di, sp, bp, ip
}

public enum Segments
{
    cs, ds, ss, es
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

public enum OneOrMoreParamsCommands
{
    push, pop
}

public enum DataDir
{
    src, dest
}

public enum IdentificatorType
{ 
    String, Digit, Binary, Hex, Char, Metka, Var, Arr, Null, Unknown
}

public enum OneWordOperations
{
    ret, cbw, cwd, cdq
}

public enum DoubleWordOperation
{
    jumps,
    je, jne, jl, jnge, jle, jng, jg, jnle, 
    jge, jnl, jb, jnae, jbe, jna, ja, jnbe, jae, jnb,
    imul, idiv, Int, inc, dec
}

public enum ThreeWordOperation
{
    mov, add, sub, and, or, xor, lea
}

public enum OperationTypeTag
{
    varDefinition,
    movData,
    cmp,
    jumps,
    oneCommandOperation,
    mathTwoParts,
    mathOneOperand,
    reg_convertions,
    metka,
    stack,
    unkn
}

public enum OperandType
{
    memory,
    register,
    segRegister,
    value
}

public static class RegTable
{
    public static Reg regAlAX = new Reg("000", (Regname.al, 0), (Regname.ax, 1));
    public static Reg regClCX = new Reg("001", (Regname.cl, 0), (Regname.cx, 1));
    public static Reg regDlDX = new Reg("010", (Regname.dl, 0), (Regname.dx, 1));
    public static Reg regBlBX = new Reg("100", (Regname.bl, 0), (Regname.bx, 1));
    public static Reg regAhSP = new Reg("100", (Regname.ah, 0), (Regname.sp, 1));
    public static Reg regCHBP = new Reg("101", (Regname.ch, 0), (Regname.bp, 1));
    public static Reg regDHSI = new Reg("110", (Regname.dh, 0), (Regname.si, 1));
    public static Reg regBHDI = new Reg("111", (Regname.bh, 0), (Regname.di, 1));

    public static Dictionary<Register, Reg> regTable = new Dictionary<Register, Reg>()
    {
        { Registers.al, regAlAX }, { Registers.ax, regAlAX },
        { Registers.cl, regClCX }, { Registers.cx, regClCX },
        { Registers.dl, regDlDX }, { Registers.dx, regDlDX },
        { Registers.bl, regBlBX }, { Registers.bx, regBlBX },
        { Registers.ah, regAhSP }, { Registers.sp, regAhSP },
        { Registers.ch, regCHBP }, { Registers.bp, regCHBP },
        { Registers.dh, regDHSI }, { Registers.si, regDHSI },
        { Registers.bh, regDHSI }, { Registers.di, regDHSI }
    };
}

public static class SgTable
{
    public static Dictionary<string, string> sg = new Dictionary<string, string>()
    {
        { "es", "00"  },
        { "cs" ,"01"  },
        { "ss" , "10" },
        { "ds", "11"  }
    };
}

public static class Mod
{

}

public static class VariablesSizes
{
    private static Dictionary<string, int> _defCommandSizeDict = new Dictionary<string, int>()
    {
        { "db", 1 },
        { "dw", 2 },
        { "dd", 4 },
        { "dq", 8 },
        { "dt", 10 }
    };

    private static Dictionary<int, string> _sizeDefCommandDict = new Dictionary<int, string>()
    {
        { 1, "db" },
        { 2, "dw" },
        { 4, "dd" },
        { 8, "dq" },
        { 10, "dt" }
    };

    public static int GetSize(string size)
    {
        return _defCommandSizeDict[size];
    }

    public static string GetDefCommand(int size)
    {
        return _sizeDefCommandDict[size];
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

