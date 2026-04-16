namespace GpsBackend.DTOs;

public record ApiResponse<T>(bool Success, T? Data = default, string? message = null);
