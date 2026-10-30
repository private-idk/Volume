using CommandSystem;
using LabApi.Features.Wrappers;
using RueI.API;
using RueI.API.Elements;
using UnityEngine;
using Volume.Systems;

namespace Volume.Commands.Client;

[CommandHandler(typeof(ClientCommandHandler))]
public class TestCommand : ICommand
{
    public string Command => "testcommand";
    public string[] Aliases => [ "testc" ];
    public string Description => "";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);

        RueDisplay.Get(player).Show(new BasicElement(300, string.Join(" ", arguments)), 2);

        response = "";
        return true;
    }
}