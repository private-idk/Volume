using Volume.CustomItems;
using Volume.Events;
using Volume.ServerSpecific;

namespace Volume;

public class Register
{
    /// <summary>
    /// Registers everything (events, custom items etc.)
    /// </summary>
    internal static void RegisterAll()
    {
        CustomItemsRegister.RegisterAll();
        BaseEvents.RegisterAll();

        ServerSpecificManager.SetOptions();
        KeybindingBase.RegisterAll();
    }

    /// <summary>
    /// Unregisters everything (events, custom items etc.)
    /// </summary>
    internal static void UnregisterAll()
    {
        CustomItemsRegister.UnregisterAll();
        BaseEvents.UnregisterAll();

        KeybindingBase.UnregisterAll();
    }
}