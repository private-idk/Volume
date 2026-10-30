using CommandSystem;
using LabApi.Features.Wrappers;
using Volume.Systems;

namespace Volume.Commands.RemoteAdmin;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class GetPlayerPlayTimeCommand : ICommand, IUsageProvider
{
    public string Command => "getplayerplaytime";
    public string[] Aliases => [ "gppt" ];
    public string Description => "Выводит проигранное игроком на сервере время [ МИНУТЫ // ЧАСЫ ]";
    public string[] Usage => [ "PLAYER_ID" ];

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count < 1)
        {
            response = "Недостаточно аргументов";
            return false;
        }
        
        if (!Int32.TryParse(arguments.At(1), out var playerId) || !Player.TryGet(playerId, out var player))
        {
            response = "Игрок с таким ID не найден";
            return false;
        }

        if (!PlaytimeCountingSystem.TryGetValue(player.UserId, out var time))
        {
            response = "Игрок впервые на сервере";
            return true;
        }

        response = $"[ {time.Minutes} // {Math.Round((double)(time.Minutes / 60))} ]";
        return true;
    }
}