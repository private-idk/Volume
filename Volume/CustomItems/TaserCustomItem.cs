using VolumeCustomItems;

namespace Volume.CustomItems;

public class TaserCustomItem : CustomItemBase
{
    public override int Id => 0;
    public override string Name => "Тазер";
    public override ItemType Type => ItemType.GunCOM18;
}