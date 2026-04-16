using GpsBackend.DTOs;

namespace GpsBackend.Interfaces;
public interface ITrackSummaryService
{
    SessionSummaryDto Build(List<TrackPointDto> track, CancellationToken cancellationToken = default);
}