using Microsoft.AspNetCore.Mvc;
using TraknovaTelematics.API.DTOs;
using TraknovaTelematics.API.Models;
using TraknovaTelematics.Core.Interfaces;

namespace TraknovaTelematics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelematicsController : ControllerBase
{
    private readonly ITelematicsIngestionService _ingestionService;
    private readonly ILogger<TelematicsController> _logger;

    public TelematicsController(
        ITelematicsIngestionService ingestionService,
        ILogger<TelematicsController> logger)
    {
        _ingestionService = ingestionService;
        _logger = logger;
    }

    /// <summary>
    /// Ingest a batch of telematics records from vehicle devices.
    /// </summary>
    /// <response code="200">All records accepted.</response>
    /// <response code="207">Partial success — some records failed validation.</response>
    /// <response code="400">All records rejected or payload is malformed.</response>
    [HttpPost("ingest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status207MultiStatus)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Ingest(
        [FromBody] List<TelematicsRecordDto> payload,
        CancellationToken ct)
    {
        if (payload is null || payload.Count == 0)
            return BadRequest(new { error = "Payload must contain at least one record." });

        _logger.LogInformation("Ingestion request received: {Count} records.", payload.Count);

        var entities = payload.Select(dto => dto.ToEntity()).ToList();
        var result = await _ingestionService.IngestAsync(entities, ct);

        var response = new
        {
            accepted = result.Accepted,
            rejected = result.Rejected,
            errors = result.Errors
        };

        if (result.Rejected == 0) return Ok(response);
        if (result.Accepted == 0) return BadRequest(response);

        // HTTP 207 Multi-Status: partial success
        return StatusCode(StatusCodes.Status207MultiStatus, response);
    }
}
