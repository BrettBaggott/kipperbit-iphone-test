using IphoneTest.Core.Models;
using IphoneTest.Core.Services;

var builder = WebApplication.CreateBuilder(args);

var storageRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "storage"));
Directory.CreateDirectory(storageRoot);
var controlPath = Path.Combine(storageRoot, "control.json");
var eventsPath = Path.Combine(storageRoot, "events.jsonl");

builder.Services.AddSingleton(new ControlStateStore(controlPath));
builder.Services.AddSingleton(new EventStore(eventsPath));

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/status", (ControlStateStore controlStore, EventStore eventStore) =>
{
    var state = controlStore.Read();
    var last = eventStore.ReadLast();
    return Results.Ok(new
    {
        enabled = state.Enabled,
        state.LastPoll,
        state.LastSuccess,
        state.LastError,
        lastEvent = last
    });
});

app.MapPost("/start", (ControlStateStore controlStore) =>
{
    var state = controlStore.Read();
    state.Enabled = true;
    controlStore.Write(state);
    return Results.Ok(new { enabled = true });
});

app.MapPost("/stop", (ControlStateStore controlStore) =>
{
    var state = controlStore.Read();
    state.Enabled = false;
    controlStore.Write(state);
    return Results.Ok(new { enabled = false });
});

app.Run();
