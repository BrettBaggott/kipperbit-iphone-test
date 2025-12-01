using System.Text.Json;
using IphoneTest.Core.Models;

namespace IphoneTest.Core.Services;

public class EventStore
{
    private readonly string _path;
    private readonly object _lock = new();
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public EventStore(string path)
    {
        _path = path;
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
    }

    public void Append(IphoneEvent evt)
    {
        lock (_lock)
        {
            var line = JsonSerializer.Serialize(evt, _jsonOptions);
            File.AppendAllText(_path, line + Environment.NewLine);
        }
    }

    public IphoneEvent? ReadLast()
    {
        if (!File.Exists(_path)) return null;
        lock (_lock)
        {
            var lines = File.ReadLines(_path);
            var last = lines.LastOrDefault();
            if (string.IsNullOrWhiteSpace(last)) return null;
            return JsonSerializer.Deserialize<IphoneEvent>(last, _jsonOptions);
        }
    }
}
