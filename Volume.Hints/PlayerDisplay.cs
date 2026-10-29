using LabApi.Features.Wrappers;

namespace VolumeHints;

public class PlayerDisplay
{
    private static Dictionary<int, PlayerDisplay> _displays = new();
    public static IReadOnlyDictionary<int, PlayerDisplay> Displays => _displays;

    internal static bool TryRemove(Player player)
    {
        if (!_displays.ContainsKey(player.PlayerId))
            return false;
        
        _displays.Remove(player.PlayerId);
        return true;
    }

    internal static bool TryAdd(Player player)
    {
        if (_displays.ContainsKey(player.PlayerId))
            return false;


        return true;
    }

    internal static void ClearDisplays() => _displays.Clear();

    public int PlayerId { get; private set; }
    
}