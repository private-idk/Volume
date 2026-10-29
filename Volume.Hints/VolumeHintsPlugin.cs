using LabApi.Loader.Features.Plugins;

namespace VolumeHints;

public class VolumeHintsPlugin : Plugin
{
    public override string Name => "Volume.Hints";
    public override string Description => string.Empty;
    public override string Author => "delta";
    public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);

    public override void Disable()
    {
    }

    public override void Enable()
    {
    }
}
