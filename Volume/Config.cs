using System.ComponentModel;

namespace Volume;

public class Config
{
    public Dictionary<ItemType, float> ArmorHealth = new()
    {
        { ItemType.ArmorLight, 25 },
        { ItemType.ArmorCombat, 50 },
        { ItemType.ArmorHeavy, 100 },
    };

    // Translations
    
    // ServerSpecific
    public string Translation_HumanRolesGroupHeader = "Опции для человеческих ролей";
    public string Translation_HumanRolesGroupHeader_CuffKeybinding = "Связать человека";
}