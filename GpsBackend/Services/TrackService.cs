using GpsBackend.DTOs;
using GpsBackend.Interfaces;

namespace GpsBackend.Services;

public class TrackService : ITrackService
{
    private const int CACHE_TIME = 30;
    private readonly ITrackRepository _trackRepository;
    private readonly ITrackStatsService _statsService;
    private readonly ITrackChartService _chartService;
    private readonly ITrackSummaryService _summaryService;
    private readonly IAppCache _cache;

    public TrackService(
        ITrackRepository trackRepository,
        ITrackStatsService statsService,
        ITrackChartService chartService,
        ITrackSummaryService summaryService,
        IAppCache cache)
    {
        _trackRepository = trackRepository;
        _statsService = statsService;
        _chartService = chartService;
        _summaryService = summaryService;
        _cache = cache;
    }

    public async Task<TrackResponseDto> LoadFromJsonAsync(
        string filePath,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"gps:full:{filePath}";

        var cached = _cache.Get<TrackResponseDto>(cacheKey);
        if (cached != null)
            return cached;

        using var doc = await _trackRepository.LoadRawAsync(filePath, cancellationToken);

        var root = doc.RootElement[0];

        var columns = root.GetProperty("columns")
            .EnumerateArray()
            .Select(c => c.GetString())
            .ToList();

        var values = root.GetProperty("values");

        int latIdx = columns.IndexOf("Lat");
        int lonIdx = columns.IndexOf("Lon");
        int sogIdx = columns.IndexOf("SOG");
        int vmgIdx = columns.IndexOf("VMG");

        var hrColumns = columns
            .Select((c, i) => new { c, i })
            .Where(x => x.c.StartsWith("HR_"))
            .Select(x => new { Id = x.c.Substring(3), x.i })
            .ToList();

        var track = new List<TrackPointDto>();

        foreach (var row in values.EnumerateArray())
        {
            cancellationToken.ThrowIfCancellationRequested();

            var arr = row.EnumerateArray().ToArray();

            var sensors = new Dictionary<string, double>();

            foreach (var hr in hrColumns)
            {
                sensors[hr.Id] = arr[hr.i].GetDouble();
            }

            track.Add(new TrackPointDto(
                arr[latIdx].GetDouble(),
                arr[lonIdx].GetDouble(),
                arr[sogIdx].GetDouble(),
                arr[vmgIdx].GetDouble(),
                sensors
            ));
        }

        var stats = _statsService.Calculate(track);
        var chart = _chartService.Build(track, cancellationToken);
        var summary = _summaryService.Build(track, cancellationToken);

        var result = new TrackResponseDto(track, stats, chart, summary);

        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(CACHE_TIME));

        return result;
    }
}