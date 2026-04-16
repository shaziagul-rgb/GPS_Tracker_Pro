namespace GpsBackend.DTOs;

public record TrackPointDto(
    double Lat,
    double Lon,
    double Sog,
    double Vmg,
    Dictionary<string, double> Sensors
);