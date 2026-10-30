using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using LiteDB;

namespace Volume.Systems;

public class PlayerTime
{
    [BsonId]
    public string UserId { get; set; }
    public int Minutes { get; set; }

    public PlayerTime()
    {
    }
    
    public PlayerTime(string userId, int time = 0)
    {
        UserId = userId;
        Minutes = time;
    }
}

public class PlaytimeCountingSystem
{
    private const string _DBFileName = "playerhours";

    private static LiteDatabase _DB;
    private static string _DBDirectoryPath = Path.Combine(PathManager.Configs.FullName, "time_count");
    private static string _DBPath = Path.Combine(_DBDirectoryPath, _DBFileName);

    private static ILiteCollection<PlayerTime> _collection;

    internal static bool TryGetValue(string userId, out PlayerTime time)
    {
        time = _collection.FindById(userId);
        return time != null;
    }
    internal static void Save(PlayerTime time) => _collection.Upsert(time);
    internal static void Remove(string userId) => _collection.Delete(userId);
    internal static void RemoveAll() => _collection.DeleteAll();

    internal static void InitializeDB()
    {
        if (!Directory.Exists(_DBDirectoryPath))
            Directory.CreateDirectory(_DBDirectoryPath);
        
        _DB = new(_DBPath);

        _collection = _DB.GetCollection<PlayerTime>(_DBFileName);
    }

    internal static void DisposeDB()
    {
        _DB.Dispose();
        _DB = null;

        _collection = null;
    }
}