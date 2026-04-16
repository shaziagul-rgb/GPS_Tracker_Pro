using GpsBackend.Configurations;
using GpsBackend.Interfaces;
using GpsBackend.Repositories;
using GpsBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<ITrackStatsService, TrackStatsService>();
builder.Services.AddScoped<ITrackRepository, FileTrackRepository>();
builder.Services.AddScoped<ITrackChartService, TrackChartService>();
builder.Services.AddScoped<ITrackSummaryService, TrackSummaryService>();
builder.Services.AddScoped<IAppCache, AppCache>();

builder.Services.AddMemoryCache();

builder.Services.Configure<TrackSettings>(
     builder.Configuration.GetSection("TrackSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("default");

app.MapControllers();

app.Run();