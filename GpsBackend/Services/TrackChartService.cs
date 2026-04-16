using GpsBackend.DTOs;
using GpsBackend.Interfaces;

namespace GpsBackend.Services;

public class TrackChartService : ITrackChartService
{
    private const int MaxPoints = 800;

    public List<TrackChartPointDto> Build(List<TrackPointDto> track, CancellationToken token = default)
    {
        if (track == null || track.Count == 0)
            return new List<TrackChartPointDto>();

        int step = Math.Max(1, track.Count / MaxPoints);
        var result = new List<TrackChartPointDto>();


        for (int i = 0; i < track.Count; i += step)
        {
            token.ThrowIfCancellationRequested();

            var p = track[i];

            result.Add(new TrackChartPointDto(
                i,
                p.Sog,
                p.Vmg,
                p.Sensors
            ));
        }

        return result;
    }
}