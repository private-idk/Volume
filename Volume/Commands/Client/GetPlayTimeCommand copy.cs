using CommandSystem;
using LabApi.Features.Wrappers;
using Volume.Systems;

namespace Volume.Commands.Client;

[CommandHandler(typeof(ClientCommandHandler))]
public class GetPlayTimeCommand : ICommand
{
    public string Command => "getplaytime";
    public string[] Aliases => [ "gpt" ];
    public string Description => "Выводит проигранное на сервере время [ МИНУТЫ // ЧАСЫ ]";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!PlaytimeCountingSystem.TryGetValue(Player.Get(sender).UserId, out var time))
        {
            response = "Вы еще не занесены в базу данных";
            return false;
        }

        response = $"[ {time.Minutes} // {Math.Round((double)(time.Minutes / 60))} ]";
        return true;
    }
}