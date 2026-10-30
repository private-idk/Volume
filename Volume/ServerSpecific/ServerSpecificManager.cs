using UserSettings.ServerSpecific;

namespace Volume.ServerSpecific;

public class ServerSpecificManager
{
    private static void NewOptions(ServerSpecificSettingBase[] options)
    {
        List<ServerSpecificSettingBase> list;

        if (ServerSpecificSettingsSync.DefinedSettings == null)
            list = new List<ServerSpecificSettingBase>();
        else
            list = ServerSpecificSettingsSync.DefinedSettings.ToList();

        List<ServerSpecificSettingBase> newOptions = list;
        newOptions.AddRange(options);

        ServerSpecificSettingsSync.DefinedSettings = newOptions.ToArray();
    }

    internal static void SetOptions()
    {
        Config config = InstanceManager.GetInstance<Config>();
        NewOptions([
            new SSGroupHeader(config.Translation_HumanRolesGroupHeader),
            new SSKeybindSetting(53, config.Translation_HumanRolesGroupHeader_CuffKeybinding)
        ]);
    }
}