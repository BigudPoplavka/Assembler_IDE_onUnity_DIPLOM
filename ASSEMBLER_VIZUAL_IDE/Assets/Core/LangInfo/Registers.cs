using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Registers
{
        public static Register al = new Register(Regname.al, 1, DescriptBase.descr_AL);
        public static Register ah = new Register(Regname.ah, 1, DescriptBase.descr_AH);
        public static Register bl = new Register(Regname.bl, 1, DescriptBase.descr_BL);
        public static Register bh = new Register(Regname.bh, 1, DescriptBase.descr_BH);
        public static Register cl = new Register(Regname.cl, 1, DescriptBase.descr_CL);
        public static Register ch = new Register(Regname.ch, 1, DescriptBase.descr_CH);
        public static Register dl = new Register(Regname.dl, 1, DescriptBase.descr_DL);
        public static Register dh = new Register(Regname.dh, 1, DescriptBase.descr_DH);

        public static Register ax = new Register(Regname.ax, 2, DescriptBase.descr_AX);
        public static Register bx = new Register(Regname.bx, 2, DescriptBase.descr_BX);
        public static Register cx = new Register(Regname.cx, 2, DescriptBase.descr_CX);
        public static Register dx = new Register(Regname.dx, 2, DescriptBase.descr_DX);

        public static Register si = new Register(Regname.si, 2, DescriptBase.descr_SI);
        public static Register di = new Register(Regname.di, 2, DescriptBase.descr_DI);
        public static Register sp = new Register(Regname.sp, 2, DescriptBase.descr_SP);
        public static Register bp = new Register(Regname.bp, 2, DescriptBase.descr_BP);
        
        public static Register CS = new Register(Regname.cs, 2, DescriptBase.descr_CS);
        public static Register DS = new Register(Regname.ds, 2, DescriptBase.descr_DS);
        public static Register ES = new Register(Regname.es, 2, DescriptBase.descr_ES);
        public static Register SS = new Register(Regname.ss, 2, DescriptBase.descr_SS);

    public static Register ip = new Register(Regname.ip, 2, DescriptBase.descr_IP);

    public static List<Register> proc_regs = new List<Register>()
        { al, ah, bl, bh, cl, ch, dl, dh, ax, bx, cx, dx, si, di, sp, bp, CS, DS, ES, SS, ip };

    public static bool IsSameSizeRegisters(string reg_1, string reg_2)
    {
        int size_1 = proc_regs.Find(x => x.RegName.ToString() == reg_1).Size;
        int size_2 = proc_regs.Find(x => x.RegName.ToString() == reg_2).Size;
        return (size_1 == size_2);
    }

    public static Register GetRegByName(string name)
    {
        return proc_regs.Find(x => x.RegName.ToString() == name);
    }

    public static Register GetRegBySize(int size)
    {
        return proc_regs.Find(x => x.Size == size);
    }

    public static string GetRegsWithSize(int size)
    {
        List<Register> tmp = proc_regs.FindAll(x => x.Size == size);
        string res = string.Empty;
        for(int i = 0; i < tmp.Count; i++)
        {
            res += tmp[i].RegName.ToString();
            if (i != tmp.Count - 1)
                res += ", ";
        }
        return res;
    }
}

