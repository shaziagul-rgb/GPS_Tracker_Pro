using GpsBackend.DTOs;
using GpsBackend.Interfaces;

namespace GpsBackend.Services;


public class TrackStatsService : ITrackStatsService
{
    private const double R = 6371;

    public TrackStatsDto Calculate(List<TrackPointDto> track, CancellationToken cancellationToken = default)
    {
        if (track == null || track.Count == 0)
            return new TrackStatsDto(0, 0, 0, new Dictionary<string, double>());

        double maxSog = double.MinValue;
        double maxVmg = double.MinValue;

        foreach (var p in track)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (p.Sog > maxSog) maxSog = p.Sog;
            if (p.Vmg > maxVmg) maxVmg = p.Vmg;
        }

        return new TrackStatsDto(
            maxSog,
            maxVmg,
            CalculateDistance(track, cancellationToken),
            CalculateSensors(track, cancellationToken)
        );
    }

    private double CalculateDistance(List<TrackPointDto> track, CancellationToken token)
    {
        double dist = 0;

        for (int i = 1; i < track.Count; i++)
        {
            token.ThrowIfCancellationRequested();
            dist += Haversine(track[i - 1], track[i]);
        }

        return dist;
    }

    private double Haversine(TrackPointDto p1, TrackPointDto p2)
    {
        double dLat = ToRad(p2.Lat - p1.Lat);
        double dLon = ToRad(p2.Lon - p1.Lon);

        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRad(p1.Lat)) * Math.Cos(ToRad(p2.Lat)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        return 2 * R * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }

    private double ToRad(double deg) => deg * Math.PI / 180;

    private Dictionary<string, double> CalculateSensors(List<TrackPointDto> track, CancellationToken token)
    {
        var result = new Dictionary<string, double>();

        foreach (var point in track)
        {
            token.ThrowIfCancellationRequested();

            if (point.Sensors == null) continue;

            foreach (var (key, value) in point.Sensors)
            {
                result[key] = result.TryGetValue(key, out var existing)
                    ? Math.Max(existing, value)
                    : value;
            }
        }

        return result;
    }
}