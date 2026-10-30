namespace Volume;

public static class InstanceManager
{
    private static readonly Dictionary<Type, object> _instances = new();
    private static readonly object _lock = new();

    /// <summary>
    /// Позволяет получить экземпляр класса
    /// </summary>
    public static T GetInstance<T>() where T : class, new()
    {
        lock (_lock)
        {
            if (!_instances.TryGetValue(typeof(T), out var instance))
                instance = _instances[typeof(T)] = new T();

            return (T)instance;
        }
    }
}
