using System.ComponentModel.DataAnnotations;
using Dragoon_Log.DTO;
using Dragoon_Log.Filter;
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
    public async Task<IActionResult> Get([FromQuery] String collection,
        [FromQuery] PaginationFilter filter)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var data = await _service.GetAllAsync(collection, validFilter);
        return Ok(new PagedResponse<List<ReceiverLog>>(
            data, validFilter.PageNumber, validFilter.PageSize, data.Count));
    }
    
    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> FilterLog(
        [FromQuery] String collection,
        [FromQuery] String key,
        [FromQuery] String value,
        [FromQuery] PaginationFilter filter)
    {
        var query = new Dictionary<String, String>(){{"key",key},{"value",value}};
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pageData = await _service.ListFilter(query, collection, validFilter);
        return Ok(new PagedResponse<List<ReceiverLog>>(
            pageData, validFilter.PageNumber, validFilter.PageSize, pageData.Count));
    }
    
}