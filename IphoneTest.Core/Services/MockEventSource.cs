using System.Text.Json;
using IphoneTest.Core.Models;

namespace IphoneTest.Core.Services;

public class MockEventSource
{
    private readonly List<IphoneEvent> _events;

    public MockEventSource(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Mock events file not found", path);

        using var stream = File.OpenRead(path);
        var loaded = JsonSerializer.Deserialize<List<IphoneEvent>>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        _events = loaded is { Count: > 0 } ? loaded : throw new InvalidOperationException("No mock events loaded");
    }

    public IphoneEvent Next(int cursor)
    {
        if (_events.Count == 0) throw new InvalidOperationException("No mock events available");
        var idx = cursor % _events.Count;
        var template = _events[idx];
        return template with { Timestamp = DateTimeOffset.UtcNow };
    }
}
