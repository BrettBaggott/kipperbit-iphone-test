using IphoneTest.Core.Models;
using IphoneTest.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IphoneTest.Worker;

public class WorkerService : BackgroundService
{
    private readonly ILogger<WorkerService> _logger;
    private readonly ControlStateStore _controlStore;
    private readonly EventStore _eventStore;
    private readonly MockEventSource _mockSource;

    public WorkerService(ILogger<WorkerService> logger, ControlStateStore controlStore, EventStore eventStore, MockEventSource mockSource)
    {
        _logger = logger;
        _controlStore = controlStore;
        _eventStore = eventStore;
        _mockSource = mockSource;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("iPhone tracker worker started.");
        while (!stoppingToken.IsCancellationRequested)
        {
            var state = _controlStore.Read();
            var now = DateTimeOffset.UtcNow;
            state.LastPoll = now;

            if (!state.Enabled)
            {
                _controlStore.Write(state);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                continue;
            }

            try
            {
                var evt = _mockSource.Next(state.SampleCursor);
                state.SampleCursor++;
                _eventStore.Append(evt);
                state.LastSuccess = now;
                state.LastError = null;
                _logger.LogInformation("Captured event {DeviceId} battery={Battery} note={Note}", evt.DeviceId, evt.Battery, evt.Note);
            }
            catch (Exception ex)
            {
                state.LastError = ex.Message;
                _logger.LogError(ex, "Failed to capture mock event");
            }

            _controlStore.Write(state);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
