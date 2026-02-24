using Microsoft.EntityFrameworkCore;
using TraknovaTelematics.Core.Interfaces;
using TraknovaTelematics.Core.Services;
using TraknovaTelematics.Infrastructure.Data;
using TraknovaTelematics.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Database
builder.Services.AddDbContext<TelematicsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure(maxRetryCount: 5)
    ));

//Application Services 
builder.Services.AddScoped<ITelematicsRepository, TelematicsRepository>();
builder.Services.AddScoped<ITelematicsIngestionService, TelematicsIngestionService>();

//API
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        //case differences in incoming JSON field names
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Traknova Telematics API", Version = "v1" });
});

//Health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<TelematicsDbContext>();

var app = builder.Build();

//Auto-migrate on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TelematicsDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

// Expose Program for integration test
public partial class Program { }
