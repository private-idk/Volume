using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using Volume.Commands.RemoteAdmin;

namespace Volume.Events;

public class MapEvents : BaseEvents
{
    private void OnIdlingTesla(PlayerIdlingTeslaEventArgs ev) => ev.IsAllowed = ToggleTeslaCommand.TeslaGatesCondition;
    private void OnTriggeringTesla(PlayerTriggeringTeslaEventArgs ev) => ev.IsAllowed = ToggleTeslaCommand.TeslaGatesCondition;
    private void OnCassieQueuingScpTermination(CassieQueuingScpTerminationEventArgs ev) => ev.IsAllowed = false;

    public override void Register()
    {
        LabApi.Events.Handlers.PlayerEvents.IdlingTesla += OnIdlingTesla;
        LabApi.Events.Handlers.PlayerEvents.TriggeringTesla += OnTriggeringTesla;
        LabApi.Events.Handlers.ServerEvents.CassieQueuingScpTermination += OnCassieQueuingScpTermination;
    }
    public override void Unregister()
    {
        LabApi.Events.Handlers.PlayerEvents.IdlingTesla -= OnIdlingTesla;
        LabApi.Events.Handlers.PlayerEvents.TriggeringTesla -= OnTriggeringTesla;
        LabApi.Events.Handlers.ServerEvents.CassieQueuingScpTermination += OnCassieQueuingScpTermination;
    }
}