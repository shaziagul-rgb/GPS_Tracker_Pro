using GpsBackend.DTOs;
namespace GpsBackend.Interfaces;

public interface ITrackService
{
    Task<TrackResponseDto> LoadFromJsonAsync(string filePath, CancellationToken cancellationToken = default);
}
