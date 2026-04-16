namespace GpsBackend.DTOs;

public record TrackResponseDto(
    List<TrackPointDto> Track,
    TrackStatsDto Stats,
    List<TrackChartPointDto> Chart,
    SessionSummaryDto Summary
);