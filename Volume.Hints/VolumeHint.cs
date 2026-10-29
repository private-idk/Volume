namespace VolumeHints;

public struct Position
{
    public Position(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    public int PosX;
    public int PosY;
}

public class VolumeHint
{
    public VolumeHint(PlayerDisplay display, string content, int time, Position pos)
    {
        Display = display;
        Content = content;
        Time = time;
        Position = pos;
    }

    public PlayerDisplay Display { get; private set; }
    public string Content { get; private set; }
    public int Time { get; private set; }
    public Position Position { get; private set; }
}