using VolumeCustomItems;

namespace Volume.CustomItems;

public static class CustomItemsRegister
{
    private static List<CustomItemBase> _items = [ InstanceManager.GetInstance<TaserCustomItem>() ];

    public static void RegisterAll()
    {
        for (int i = 0; i < _items.Count; i++)
            _items[i].TryRegister();
    }
    public static void UnregisterAll()
    {
        for (int i = 0; i < _items.Count; i++)
            _items[i].TryRemove();
    }
}