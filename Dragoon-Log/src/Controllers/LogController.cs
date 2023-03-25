using System.ComponentModel.DataAnnotations;
using Dragoon_Log.DTO;
using Dragoon_Log.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dragoon_Log.Controllers;

[ApiController]
[Route("log")]
public class LogController : ControllerBase
{
    private ILogService _service;

    public LogController(ILogService logService)
    {
        _service = logService;
    }
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<List<ReceiverLog>> Get()
    {
        return _service.GetAllAsync();
    }
    
    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<List<ReceiverLog>> Filter(
        [FromQuery][Required]Dictionary<String, String> filter)
    {
        return _service.ListFilter(filter);
    }
    
}