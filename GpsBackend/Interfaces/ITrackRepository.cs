using System.Text.Json;
namespace GpsBackend.Interfaces;

public interface ITrackRepository
{
    Task<JsonDocument> LoadRawAsync(string path,CancellationToken cancellationToken=default);
}