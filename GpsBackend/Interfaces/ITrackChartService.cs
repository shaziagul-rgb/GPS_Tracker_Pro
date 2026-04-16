using GpsBackend.DTOs;

namespace GpsBackend.Interfaces;

public interface ITrackChartService
{
    List<TrackChartPointDto> Build(List<TrackPointDto> track, CancellationToken cancellationToken = default);
}