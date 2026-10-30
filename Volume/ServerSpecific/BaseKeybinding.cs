using LabApi.Features.Wrappers;
using UserSettings.ServerSpecific;

namespace Volume.ServerSpecific;

public abstract class KeybindingBase
{
    private static readonly List<KeybindingBase> _keybindings = 
    [ 
        
    ];

    public static void RegisterAll()
    {
        for (int i = 0; i < _keybindings.Count; i++)
            _keybindings[i].Register();
    }
    public static void UnregisterAll()
    {
        for (int i = 0; i < _keybindings.Count; i++)
            _keybindings[i].Unregister();
    }

    /// <summary>
    /// Keybinding ID.
    /// </summary>
    public abstract int Id { get; }

    /// <summary>
    /// Method called when a corresponding key is pressed.
    /// </summary>
    /// <param name="player"></param>
    public abstract void Method(Player player);

    private void ProcessInput(ReferenceHub hub, ServerSpecificSettingBase settings)
    {
        if (settings is not SSKeybindSetting keybindSetting || !keybindSetting.SyncIsPressed || keybindSetting.SettingId != Id) return;

        Method(Player.Get(hub));
    }

    public void Register() => ServerSpecificSettingsSync.ServerOnSettingValueReceived += ProcessInput;
    public void Unregister() => ServerSpecificSettingsSync.ServerOnSettingValueReceived -= ProcessInput;
}