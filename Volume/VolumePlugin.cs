using LabApi.Loader.Features.Plugins;

namespace Volume;

public class VolumePlugin : Plugin<Config>
{
    public override string Name => "Volume";
    public override string Description => string.Empty;
    public override string Author => "delta";
    public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);

    public override void Disable()
    {
        Register.UnregisterAll();
    }

    public override void Enable()
    {
        Register.RegisterAll();
    }
}
