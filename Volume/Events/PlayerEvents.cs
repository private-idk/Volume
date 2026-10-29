using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Features.Wrappers;
using Volume.Systems;

namespace Volume.Events;

public class PlayerEvents : BaseEvents
{
    private void OnItemAdded(ReferenceHub hub, ItemBase itemBase, ItemPickupBase pickupBase)
    {
        if (!InstanceManager.GetInstance<Config>().ArmorHealth.TryGetValue(itemBase.ItemTypeId, out float maxHealth))
            return;

        Player player = Player.Get(hub);

        if (!BodyarmorSystem.ArmorIntegrity.ContainsKey(itemBase.ItemSerial))
            BodyarmorSystem.TryAddArmor(itemBase.ItemSerial);

        float health = BodyarmorSystem.ArmorIntegrity[itemBase.ItemSerial];
        player.MaxHumeShield = maxHealth;

        if (health == 0)
            return;

        player.HumeShield = health;
    }

    private void OnItemRemoved(ReferenceHub hub, ItemBase itemBase, ItemPickupBase pickupBase)
    {
        if (!InstanceManager.GetInstance<Config>().ArmorHealth.ContainsKey(itemBase.ItemTypeId))
            return;

        Player player = Player.Get(hub);

        BodyarmorSystem.TryChangeIntegrity(itemBase.ItemSerial, player.HumeShield);
        
        player.HumeShield = 0;
        player.MaxHumeShield = 0;
    }

    public override void Register()
    {
        InventoryExtensions.OnItemAdded += OnItemAdded;
        InventoryExtensions.OnItemRemoved += OnItemRemoved;
    }

    public override void Unregister()
    {
        InventoryExtensions.OnItemAdded -= OnItemAdded;
        InventoryExtensions.OnItemRemoved -= OnItemRemoved;
    }
}