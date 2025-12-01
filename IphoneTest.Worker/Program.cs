using IphoneTest.Core.Services;
using IphoneTest.Worker;

var builder = Host.CreateApplicationBuilder(args);

var storageRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "storage"));
Directory.CreateDirectory(storageRoot);
var dataPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "data", "mock-events.json"));

builder.Services.AddSingleton(new ControlStateStore(Path.Combine(storageRoot, "control.json")));
builder.Services.AddSingleton(new EventStore(Path.Combine(storageRoot, "events.jsonl")));
builder.Services.AddSingleton(new MockEventSource(dataPath));

builder.Services.AddHostedService<WorkerService>();

var host = builder.Build();
host.Run();
