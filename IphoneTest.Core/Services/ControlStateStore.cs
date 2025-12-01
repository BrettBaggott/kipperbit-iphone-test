using System.Text.Json;
using IphoneTest.Core.Models;

namespace IphoneTest.Core.Services;

public class ControlStateStore
{
    private readonly string _path;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web) { WriteIndented = true };
    private readonly object _lock = new();

    public ControlStateStore(string path)
    {
        _path = path;
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
    }

    public ControlState Read()
    {
        lock (_lock)
        {
            if (!File.Exists(_path))
            {
                var initial = new ControlState();
                Write(initial);
                return initial;
            }

            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<ControlState>(json, _jsonOptions) ?? new ControlState();
        }
    }

    public void Write(ControlState state)
    {
        lock (_lock)
        {
            var json = JsonSerializer.Serialize(state, _jsonOptions);
            File.WriteAllText(_path, json);
        }
    }
}
