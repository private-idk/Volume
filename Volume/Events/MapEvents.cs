using LabApi.Events.Arguments.PlayerEvents;
using Volume.Commands.RemoteAdmin;

namespace Volume.Events;

public class MapEvents : BaseEvents
{
    private void OnIdlingTesla(PlayerIdlingTeslaEventArgs ev) => ev.IsAllowed = ToggleTeslaCommand.TeslaGatesCondition;
    private void OnTriggeringTesla(PlayerTriggeringTeslaEventArgs ev) => ev.IsAllowed = ToggleTeslaCommand.TeslaGatesCondition;

    public override void Register()
    {
        LabApi.Events.Handlers.PlayerEvents.IdlingTesla += OnIdlingTesla;
        LabApi.Events.Handlers.PlayerEvents.TriggeringTesla += OnTriggeringTesla;
    }
    public override void Unregister()
    {
        LabApi.Events.Handlers.PlayerEvents.IdlingTesla -= OnIdlingTesla;
        LabApi.Events.Handlers.PlayerEvents.TriggeringTesla -= OnTriggeringTesla;
    }
}