using System.ComponentModel;

namespace Volume;

public class Config
{
    [Description("Количество брони бронежилета")]
    public Dictionary<ItemType, float> ArmorHealth = new()
    {
        { ItemType.ArmorLight, 25 },
        { ItemType.ArmorCombat, 50 },
        { ItemType.ArmorHeavy, 100 },
    };
}