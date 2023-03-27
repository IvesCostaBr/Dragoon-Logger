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
    public Task<List<ReceiverLog>> Get([FromQuery] String collection)
    {
        Console.WriteLine(collection);
        return _service.GetAllAsync(collection);
    }
    
    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<List<ReceiverLog>> Filter(
        [FromQuery][Required]Dictionary<String, String> filter, 
        [FromQuery] String collection)
    {
        return _service.ListFilter(filter, collection);
    }
    
}