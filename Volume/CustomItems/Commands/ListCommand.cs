using System.Text;
using CommandSystem;
using VolumeCustomItems;

namespace Volume.CustomItems.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ListCommand : ICommand
{
    private static readonly Lazy<string> CachedResponse = new(CreateResponse);

    public string Command => "listitems";
    public string[] Aliases => [ "li" ];
    public string Description => "Выводит список кастомных предметов";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = CachedResponse.Value;
        return true;
    }

    private static string CreateResponse()
    {
        StringBuilder sb = new();

        sb.AppendLine("Список кастомных предметов:");
        for (int i = 0; i < CustomItemBase.Items.Count; i++)
            sb.AppendLine($"[{CustomItemBase.Items[i].Id}] {CustomItemBase.Items[i].Name} - {CustomItemBase.Items[i].Description}");
        
        return sb.ToString();
    }
}
