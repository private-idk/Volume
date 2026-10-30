using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using MEC;
using VolumeCustomItems;

namespace Volume.CustomItems;

public class TaserCustomItem : CustomItemBase
{
    public override int Id => 0;
    public override string Name => "Тазер";
    public override string Description => "Ненадолго оглушает человека";
    public override ItemType Type => ItemType.GunCOM18;

    protected override void RegisterEvents()
    {
        LabApi.Events.Handlers.PlayerEvents.ReloadedWeapon += OnReloadedWeapon;
        LabApi.Events.Handlers.PlayerEvents.Hurting += OnHurting;

        base.RegisterEvents();
    }
    protected override void UnregisterEvents()
    {
        LabApi.Events.Handlers.PlayerEvents.ReloadedWeapon -= OnReloadedWeapon;
        LabApi.Events.Handlers.PlayerEvents.Hurting -= OnHurting;

        base.UnregisterEvents();
    }

    private void OnHurting(PlayerHurtingEventArgs ev)
    {
        if (InstanceManager.GetInstance<Config>().NonTaserableRoles.Contains(ev.Player.Role))
            return;

        ev.IsAllowed = false;

        Timing.RunCoroutine(ShakeCamera(ev.Player));
    }
    private void OnReloadedWeapon(PlayerReloadedWeaponEventArgs ev)
    {
        if (!Check(ev.FirearmItem))
            return;

        if (ev.FirearmItem.StoredAmmo > 3)
            ev.FirearmItem.StoredAmmo = 3;

        ev.Player.AddAmmo(ev.FirearmItem.Type, (ushort)(ev.FirearmItem.MaxAmmo - ev.FirearmItem.StoredAmmo));
    }
    protected override void OnGiven(Player player, Item item)
    {
        FirearmItem firearm = item as FirearmItem;

        if (firearm.StoredAmmo > 3)
            firearm.StoredAmmo = 3;

        base.OnGiven(player, item);
    }

    private IEnumerator<float> ShakeCamera(Player player)
    {
        int lifeId = player.ReferenceHub.roleManager.CurrentRole.UniqueLifeIdentifier;

        player.CurrentItem = null;

        bool isCuffed = player.IsDisarmed;

        player.IsDisarmed = true;

        for (int i = 0; i < 101; i++)
        {
            yield return Timing.WaitForSeconds(0.1f);

            if (player.ReferenceHub.roleManager.CurrentRole.UniqueLifeIdentifier != lifeId)
                break;

            player.Rotation = UnityEngine.Random.rotation;

            if (i % 20 == 0)
            {
                player.ReferenceHub.playerEffectsController.TryGetEffect("Flashed", out var effect);
                effect.ServerSetState(1, 1, true);
            }
        }

        player.IsDisarmed = isCuffed;
    }
}