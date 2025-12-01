namespace IphoneTest.Core.Models;

public record IphoneEvent(
    string DeviceId,
    DateTimeOffset Timestamp,
    double? Battery,
    double? Latitude,
    double? Longitude,
    string? Note);
