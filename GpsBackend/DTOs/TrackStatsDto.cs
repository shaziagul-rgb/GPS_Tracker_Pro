namespace GpsBackend.DTOs;

public record TrackStatsDto(
    double MaxSog,
    double MaxVmg,
    double TotalDistance,
    Dictionary<string, double> MaxSensors
);