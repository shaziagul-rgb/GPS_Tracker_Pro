namespace GpsBackend.DTOs;

public record TrackChartPointDto(
    int Index,
    double Sog,
    double Vmg,
    Dictionary<string, double>? Sensors
);