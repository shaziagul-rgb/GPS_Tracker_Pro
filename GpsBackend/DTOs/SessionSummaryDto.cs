namespace GpsBackend.DTOs;

public record SessionSummaryDto(
    double TotalDistanceKm,
    double AvgSpeed,
    double MaxSpeed,
    double MaxHeartRate,
    int SampleCount,
    double DurationSeconds
);