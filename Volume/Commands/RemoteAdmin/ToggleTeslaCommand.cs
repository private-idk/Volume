using CommandSystem;

namespace Volume.Commands.RemoteAdmin;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ToggleTeslaCommand : ICommand
{
    public static bool TeslaGatesCondition { get; private set; } = false;

    public string Command => "tesla";
    public string[] Aliases => [];
    public string Description => "Переключает тесла-ворота";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        TeslaGatesCondition = !TeslaGatesCondition;
        
        response = TeslaGatesCondition ? "Ворота были включены" : "Ворота были выключены";
        return true;
    }
}