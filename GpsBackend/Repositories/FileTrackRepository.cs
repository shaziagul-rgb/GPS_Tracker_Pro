
using System.Text.Json;
using GpsBackend.Interfaces;

namespace GpsBackend.Repositories
{
    public class FileTrackRepository : ITrackRepository
    {
        public async Task<JsonDocument> LoadRawAsync(string path,
                                                     CancellationToken cancellationToken)
        {
            var json = await File.ReadAllTextAsync(path, cancellationToken);
            return JsonDocument.Parse(json);
        }
    }
}