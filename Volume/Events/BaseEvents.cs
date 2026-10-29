namespace Volume.Events;

public abstract class BaseEvents
{
    private static readonly List<BaseEvents> _events = 
    [ 
        InstanceManager.GetInstance<PlayerEvents>(),
        InstanceManager.GetInstance<ServerEvents>(),
        InstanceManager.GetInstance<MapEvents>()
    ];

    public static void RegisterAll()
    {
        for (int i = 0; i < _events.Count; i++)
            _events[i].Register();
    }
    public static void UnregisterAll()
    {
        for (int i = 0; i < _events.Count; i++)
            _events[i].Unregister();
    }

    public abstract void Register();
    public abstract void Unregister();
}