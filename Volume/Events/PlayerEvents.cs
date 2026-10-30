using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using Volume.Systems;

namespace Volume.Events;

public class PlayerEvents : BaseEvents
{
    private Dictionary<string, int> _joinedTime;

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

    // Hour counting system
    private void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        if (!PlayTimeCountingSystem.TryGetValue(ev.Player.UserId, out _))
            PlayTimeCountingSystem.Save(new(ev.Player.UserId));

        _joinedTime[ev.Player.UserId] = DateTime.Now.Minute;
    }
    private void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        if (!PlayTimeCountingSystem.TryGetValue(ev.Player.UserId, out var time))
            return;
        
        time.Minutes += _joinedTime[ev.Player.UserId];
        _joinedTime.Remove(ev.Player.UserId);

        PlayTimeCountingSystem.Save(time);
    }

    public override void Register()
    {
        _joinedTime = new();

        InventoryExtensions.OnItemAdded += OnItemAdded;
        InventoryExtensions.OnItemRemoved += OnItemRemoved;
        LabApi.Events.Handlers.PlayerEvents.Joined += OnPlayerJoined;
        LabApi.Events.Handlers.PlayerEvents.Left += OnPlayerLeft;
    }

    public override void Unregister()
    {
        _joinedTime = null;

        InventoryExtensions.OnItemAdded -= OnItemAdded;
        InventoryExtensions.OnItemRemoved -= OnItemRemoved;
        LabApi.Events.Handlers.PlayerEvents.Joined -= OnPlayerJoined;
        LabApi.Events.Handlers.PlayerEvents.Left -= OnPlayerLeft;
    }
}