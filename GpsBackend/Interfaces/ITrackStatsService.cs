using GpsBackend.DTOs;

namespace GpsBackend.Interfaces;

public interface ITrackStatsService
{
    TrackStatsDto Calculate(List<TrackPointDto> track, CancellationToken cancellationToken = default);
}