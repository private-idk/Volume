using LabApi.Features.Console;
using LabApi.Features.Wrappers;

namespace Volume.Systems;

public class BodyarmorSystem
{
    private static Dictionary<ushort, float> _armorIntegrity = new();
    public static IReadOnlyDictionary<ushort, float> ArmorIntegrity => _armorIntegrity;

    /// <summary>
    /// Clears armor pool
    /// </summary>
    internal static void ClearIntegrity() => _armorIntegrity.Clear();

    /// <summary>
    /// Adds armor to pool with max corresponding value
    /// </summary>
    /// <param name="serial">Item serial</param>
    /// <returns>Is successful</returns>
    internal static bool TryAddArmor(ushort serial)
    {
        if (!Item.TryGet(serial, out Item item))
        {
            Logger.Error($"ITEM WITH SERIAL {serial} NOT EXISTS");
            return false;
        }

        if (!InstanceManager.GetInstance<Config>().ArmorHealth.TryGetValue(item.Type, out var health))
        {
            Logger.Error($"ITEM WITH SERIAL {serial} IS NOT ARMOR");
            return false;
        }

        _armorIntegrity[serial] = health;

        return true;
    }

    /// <summary>
    /// Changes armor integrity in pool
    /// </summary>
    /// <param name="serial">Item serial</param>
    /// <param name="newHealth">New integrity</param>
    /// <returns>Is successful</returns>
    internal static bool TryChangeIntegrity(ushort serial, float newHealth)
    {
        if (!_armorIntegrity.ContainsKey(serial))
        {
            Logger.Error($"ARMOR WITH SERIAL {serial} WAS NOT FOUND");
            return false;
        }

        _armorIntegrity[serial] = newHealth;
        return true;
    }
}