namespace IphoneTest.Core.Models;

public class ControlState
{
    public bool Enabled { get; set; } = true;
    public DateTimeOffset? LastPoll { get; set; }
    public DateTimeOffset? LastSuccess { get; set; }
    public string? LastError { get; set; }
    public int SampleCursor { get; set; } = 0;
}
