namespace SpaceBattle.Lib;

class MacroCommand: ICommand
{
    private IEnumerable<ICommand> command_list;

    public MacroCommand(params ICommand[] command_list)
    {
        this.command_list = command_list;
    }
    public MacroCommand(IEnumerable<ICommand> command_list)
    {
        this.command_list = command_list;
    }

    public void Execute()
    {
        command_list.ToList().ForEach(cmd => cmd.Execute());
    }
}
