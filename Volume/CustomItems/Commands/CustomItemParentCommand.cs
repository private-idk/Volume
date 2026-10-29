using System.Text;
using CommandSystem;

namespace Volume.CustomItems.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class CustomItemParentCommand : ParentCommand
{
    private ICommand[] _commands;

    public CustomItemParentCommand() => LoadGeneratedCommands();
    public override string Command => "customitem";
    public override string[] Aliases => [ "ci" ];
    public override string Description => string.Empty;

    public override void LoadGeneratedCommands()
    {
        _commands = [ new GiveCommand(), new ListCommand() ];
        
        for (int i = 0; i < _commands.Length; i++)
            RegisterCommand(_commands[i]);
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        StringBuilder sb = new();
        sb.AppendLine("Список доступных команд:");
        for (int i = 0; i < _commands.Length; i++)
            sb.AppendLine($"{_commands[i].Command} [{_commands[i].Aliases.ToString()}] - {_commands[i].Description}");

        response = sb.ToString();
        return false;
    }
}