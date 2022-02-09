using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Command
{
    private Commands _command_word;
    private int _params_count;
    private int _priority;
    private List<Flag> _enfl_flags;

    public Command(Commands command, int params_cnt, int priority, List<Flag> flags)
    {
        Command_word = command;
        Params_count = params_cnt;
        Priority = priority;
        Enfl_flags = flags;
    }

    public Commands Command_word { get => _command_word; set => _command_word = value; }
    public int Params_count { get => _params_count; set => _params_count = value; }
    public int Priority { get => _priority; set => _priority = value; }
    public List<Flag> Enfl_flags { get => _enfl_flags; set => _enfl_flags = value; }
}
