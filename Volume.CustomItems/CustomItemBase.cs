using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;

namespace VolumeCustomItems;

/// <summary>
/// Наследуемые классы должны оканчиваться на CustomItem
/// </summary>
public abstract class CustomItemBase
{
    private static Dictionary<int, CustomItemBase> _items = new();
    private static Dictionary<ushort, CustomItemBase> _spawnedItems = new();

    /// <summary>
    /// Содержит зарегистрированные кастомные предметы.
    /// </summary>
    public static IReadOnlyDictionary<int, CustomItemBase> Items => _items;

    /// <summary>
    /// Содержит существующие кастомные предметы
    /// </summary>
    public static IReadOnlyDictionary<ushort, CustomItemBase> SpawnedItems => _spawnedItems;

    private static bool TryAdd(CustomItemBase item)
    {
        if (_items.ContainsKey(item.Id))
        {
            Logger.Error($"ITEM WITH SAME ID ({item.Id}) ALREADY EXISTS");
            return false;
        }

        Logger.Info($"ITEM WITH ID ({item.Id}) WAS JUST ADDED");

        _items[item.Id] = item;
        return true;
    }

    private static bool TryRemove(int id)
    {
        if (!_items.ContainsKey(id))
        {
            Logger.Error($"ITEM WITH ID ({id}) NOT EXISTS");
            return false;
        }

        Logger.Info($"ITEM WITH ID ({id}) WAS JUST REMOVED");

        _items.Remove(id);
        return true;
    }

    public abstract int Id { get; }
    public abstract string Name { get; }
    public abstract ItemType Type { get; }

    /// <summary>
    /// Регистрирует кастомный предмет и его ивенты.
    /// </summary>
    public void TryRegister()
    {
        if (!TryAdd(this))
            return;
        
        RegisterEvents();
    }

    /// <summary>
    /// Удаляет кастомный предмет и его ивенты.
    /// </summary>
    public void TryRemove()
    {
        if (!TryRemove(Id))
            return;
        
        UnregisterEvents();
    }

    /// <summary>
    /// Выдает предмет игроку. Если инвентарь полон, то предмет не будет выдан.
    /// </summary>
    /// <param name="player">Игрок, которому будет выдан предмет.</param>
    public void GiveItem(Player player)
    {
        if (player.IsInventoryFull)
            return;

        _spawnedItems[player.AddItem(Type).Serial] = this;
    }
    
    /// <summary>
    /// Проверяет является ли предмет кастомным.
    /// </summary>
    /// <param name="item">Предмет для проверки</param>
    public bool Check(Item item) => SpawnedItems.ContainsKey(item.Serial);

    /// <summary>
    /// Регистрирует ивенты для кастомного предмета. Вызывается при регистрации такого предмета.
    /// .base отвечает за регистрацию ивентов, которые вызывают hint'ы, получаемые игроком при получении предмета.
    /// </summary>
    protected virtual void RegisterEvents()
    {
        LabApi.Events.Handlers.PlayerEvents.PickedUpItem += OnPickedUpItem;
    }

    /// <summary>
    /// Удаляет ивенты для кастомного предмета. Вызывается при удалении такого предмета.
    /// .base отвечает за удаления ивентов, который вызывают hint'ы, получаемые игроком при получении предмета.
    /// </summary>
    protected virtual void UnregisterEvents()
    {
        LabApi.Events.Handlers.PlayerEvents.PickedUpItem -= OnPickedUpItem;
    }

    /// <summary>
    /// Вызывается при получении предмета игроком.
    /// .base отвечает за hint'ы, получаемые игроком при получении предмета.
    /// </summary>
    protected virtual void OnGiven(Player player)
    {
        // Сделать хинты
    }

    private void OnPickedUpItem(PlayerPickedUpItemEventArgs ev) => OnGiven(ev.Player);
}