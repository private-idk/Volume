using LabApi.Loader.Features.Plugins;

namespace VolumeCustomItems;

public class VolumeCustomItemsPlugin : Plugin
{
    public override string Name => "Volume.CustomItems";
    public override string Description => string.Empty;
    public override string Author => "delta";
    public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);

    public override void Disable()
    {
        LabApi.Events.Handlers.ServerEvents.RoundStarted -= OnRoundStarted;
    }

    public override void Enable()
    {
        LabApi.Events.Handlers.ServerEvents.RoundStarted += OnRoundStarted;
    }

    public void OnRoundStarted() => CustomItemBase.ClearSpawnedItems();
}