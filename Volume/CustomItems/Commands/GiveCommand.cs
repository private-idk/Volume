using CommandSystem;
using LabApi.Features.Wrappers;
using VolumeCustomItems;

namespace Volume.CustomItems.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class GiveCommand : ICommand, IUsageProvider
{
    public string Command => "give";
    public string[] Aliases => [ "g" ];
    public string Description => "Выдает кастомный предмет игроку";
    public string[] Usage => [ "PLAYER_ID", "ITEM_ID" ];

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count < 2)
        {
            response = "Мало аргументов";
            return false;
        }

        if (!Int32.TryParse(arguments.At(0), out var pid) || !Player.TryGet(pid, out var player))
        {
            response = "Игрок с таким ID не найден";
            return false;
        }

        if (!Int32.TryParse(arguments.At(1), out var iid) || !CustomItemBase.Items.TryGetValue(iid, out var item))
        {
            response = "Предмет с таким ID не найден";
            return false;
        }

        item.GiveItem(player);

        response = $"{item.Name} выдан [{player.PlayerId}] {player.Nickname}";
        return true;
    }
}