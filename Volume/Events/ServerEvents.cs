using CommandSystem.Commands.RemoteAdmin;
using LabApi.Features.Wrappers;

namespace Volume.Events;

public class ServerEvents : BaseEvents
{
    private void OnWaitingForPlayers()
    {
        Round.IsLobbyLocked = true;
        Round.IsLocked = true;
    }

    public override void Register()
    {
        LabApi.Events.Handlers.ServerEvents.WaitingForPlayers += OnWaitingForPlayers;
    }
    public override void Unregister()
    {
        LabApi.Events.Handlers.ServerEvents.WaitingForPlayers -= OnWaitingForPlayers;
    }
}