using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.CLI.Commands;

public abstract class CommandArgumentExec
{
    public string Arg;
    public Action<string[]> Exec;

    protected CommandArgumentExec(string arg, Action<string[]> exec)
    {
        Arg = arg;
        Exec = exec;
    }

    public bool TryMatch(string[] args)
    {
        string[] command = Arg.Split(' ');

        try
        {
            int count = 0;

            while (count < command.Length)
            {
                if (args[count] != command[count])
                    return (false);
                ++count;
            }
        }
        catch (Exception)
        {
            return (false);
        }

        return (true);
    }
}