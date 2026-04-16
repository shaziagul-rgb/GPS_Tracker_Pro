using GpsBackend.DTOs;
using GpsBackend.Interfaces;

namespace GpsBackend.Services;

public class TrackSummaryService : ITrackSummaryService
{
    public SessionSummaryDto Build(List<TrackPointDto> track, CancellationToken token = default)
    {
        if (track == null || track.Count == 0)
            return new SessionSummaryDto(0, 0, 0, 0, 0, 0);

        token.ThrowIfCancellationRequested();

        var speeds = track.Select(x => x.Sog).ToList();

        var heartRates = track
            .SelectMany(t => t.Sensors ?? new Dictionary<string, double>())
            .Where(x => x.Key.Contains("HR"))
            .Select(x => x.Value)
            .ToList();

        return new SessionSummaryDto(
            TotalDistanceKm: 0,
            AvgSpeed: speeds.Average(),
            MaxSpeed: speeds.Max(),
            MaxHeartRate: heartRates.Count > 0 ? heartRates.Max() : 0,
            SampleCount: track.Count,
            DurationSeconds: track.Count
        );
    }
}