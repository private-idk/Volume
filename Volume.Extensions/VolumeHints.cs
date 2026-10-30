using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using RueI.API;
using RueI.API.Elements;

namespace VolumeExtensions;

public static class VolumeHints
{
    private static readonly Dictionary<int, Tag> _existsHints = new ();
    
    /// <summary>
    /// Отправляет хинт игроку через RueI
    /// </summary>
    /// <param name="player">Игрок</param>
    /// <param name="text">Содержание хинта</param>
    /// <param name="time">Время, которое он просуществует</param>
    public static void SendHint(Player player, string text, float time = 1.5f)
    {
        var display = RueDisplay.Get(player);
        if (_existsHints.TryGetValue(player.PlayerId, out var tag))
        {
            try
            {
                display.Remove(tag);
            } catch (Exception e) {}
            _existsHints.Remove(player.PlayerId);
        }

        Tag playerTag = new();
        display.Show(playerTag, new BasicElement(250f, $"<b>{text}</b>"), time);
        _existsHints.Add(player.PlayerId, playerTag);
    }
}