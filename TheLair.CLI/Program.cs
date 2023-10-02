using TheLair.CLI.Commands;
using TheLair.CLI.Commands.API;

CommandArgumentExec[] exec = new CommandArgumentExec[]
{
    new ScaffoldApiClass()
};

foreach (CommandArgumentExec command in exec)
{
    if (command.TryMatch(args))
    {
        command.Exec(args);
        return;
    }
}

Console.WriteLine("Usage:");

foreach (CommandArgumentExec command in exec)
{
    Console.WriteLine($"- {command.Arg}");
}