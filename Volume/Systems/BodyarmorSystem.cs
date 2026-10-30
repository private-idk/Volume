using LabApi.Features.Console;
using LabApi.Features.Wrappers;

namespace Volume.Systems;

public class BodyarmorSystem
{
    private static Dictionary<ushort, float> _armorIntegrity = new();
    public static IReadOnlyDictionary<ushort, float> ArmorIntegrity => _armorIntegrity;

    internal static void ClearIntegrity() => _armorIntegrity.Clear();

    /// <summary>
    /// Добавляет броню в словарь с максимальным возможным значением
    /// </summary>
    /// <param name="serial">Серийный номер предмета</param>
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
    /// Изменяет прочность брони
    /// </summary>
    /// <param name="serial">Серийный номер предмета</param>
    /// <param name="newHealth">Новая прочность</param>
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