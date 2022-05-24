using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Flags
{
    public static Flag CF = new Flag(RegNameFlags.CF, 0, DescriptBase.descr_CF, false);
    public static Flag PF = new Flag(RegNameFlags.PF, 2, DescriptBase.descr_PF, false);
    public static Flag AF = new Flag(RegNameFlags.AF, 4, DescriptBase.descr_AF, false);
    public static Flag ZF = new Flag(RegNameFlags.ZF, 6, DescriptBase.descr_ZF, false);
    public static Flag SF = new Flag(RegNameFlags.SF, 7, DescriptBase.descr_SF, false);
    public static Flag OF = new Flag(RegNameFlags.OF, 11, DescriptBase.descr_OF, false);
    public static Flag DF = new Flag(RegNameFlags.DF, 10, DescriptBase.descr_DF, false);
    public static Flag TF = new Flag(RegNameFlags.TF, 8, DescriptBase.descr_TF, false);
    public static Flag IF = new Flag(RegNameFlags.IF, 9, DescriptBase.descr_IF, false);
    public static Flag IOPL_A = new Flag(RegNameFlags.IOPL, 12, DescriptBase.descr_IOPL, false);
    public static Flag IOPL_B = new Flag(RegNameFlags.IOPL, 13, DescriptBase.descr_IOPL, false);
    public static Flag RF = new Flag(RegNameFlags.RF, 16, DescriptBase.descr_RF, false);
    public static Flag NT = new Flag(RegNameFlags.NT, 14, DescriptBase.descr_NT, false);
    public static Flag AC = new Flag(RegNameFlags.AC, 18, DescriptBase.descr_AC, false);
    public static Flag VM = new Flag(RegNameFlags.VM, 17, DescriptBase.descr_VM, false);
}

