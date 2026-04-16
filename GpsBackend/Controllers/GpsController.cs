
using GpsBackend.Configurations;
using GpsBackend.DTOs;
using GpsBackend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpsBackend.Controllers;

[ApiController]
[Route("api/gps")]
public class GpsController : ControllerBase
{
    private readonly ITrackService _service;
    private readonly TrackSettings _settings;

    public GpsController(ITrackService service, IOptions<TrackSettings> options)
    {
        _service = service;
        _settings = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                _settings.FilePath ?? "DataFiles/data.json"
            );

            var result = await _service.LoadFromJsonAsync(fullPath,cancellationToken);

            return Ok(new ApiResponse<TrackResponseDto>(
                true,
                result,
                "Track loaded successfully"
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<string>(
                false,
                null,
                ex.Message
            ));
        }
    }
}