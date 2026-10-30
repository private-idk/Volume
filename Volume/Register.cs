using Volume.CustomItems;
using Volume.Events;
using Volume.ServerSpecific;
using Volume.Systems;

namespace Volume;

public class Register
{
    internal static void RegisterAll()
    {
        CustomItemsRegister.RegisterAll();
        BaseEvents.RegisterAll();

        ServerSpecificManager.SetOptions();
        KeybindingBase.RegisterAll();

        PlaytimeCountingSystem.InitializeDB();
    }

    internal static void UnregisterAll()
    {
        CustomItemsRegister.UnregisterAll();
        BaseEvents.UnregisterAll();

        KeybindingBase.UnregisterAll();

        PlaytimeCountingSystem.DisposeDB();
    }
}