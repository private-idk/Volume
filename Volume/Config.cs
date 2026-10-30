using System.ComponentModel;
using PlayerRoles;

namespace Volume;

public class Config
{
    public Dictionary<ItemType, float> ArmorHealth = new()
    {
        { ItemType.ArmorLight, 25 },
        { ItemType.ArmorCombat, 50 },
        { ItemType.ArmorHeavy, 100 },
    };
    public List<RoleTypeId> NonTaserableRoles = new()
    {
        RoleTypeId.Scp079, RoleTypeId.Scp096, RoleTypeId.Scp106, RoleTypeId.Scp173, RoleTypeId.Scp3114, RoleTypeId.Scp939
    };

    // Translations
    
    // ServerSpecific
    public string Translation_HumanRolesGroupHeader = "Опции для человеческих ролей";
    public string Translation_HumanRolesGroupHeader_CuffKeybinding = "Связать человека";
}