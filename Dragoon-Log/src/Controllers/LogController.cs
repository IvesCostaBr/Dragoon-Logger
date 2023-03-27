using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
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
    public async Task<IActionResult> Get(
        [FromQuery] PaginationFilter filter,
        [FromQuery] string? collection)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        if (collection == null)
        {
            return Ok(new PagedResponse<List<ReceiverLog>>(
                null, validFilter.PageNumber, validFilter.PageSize, 0));
        }
        var data = await _service.GetAllAsync(collection, validFilter);
        return Ok(new PagedResponse<List<ReceiverLog>>(
            data, validFilter.PageNumber, validFilter.PageSize, data.Count));
    }
    
    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> FilterLog([FromQuery] string key,
        [FromQuery] string value,
        [FromQuery] PaginationFilter filter,
        [FromQuery] string? collection)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        if (collection == null)
        {
            return Ok(new PagedResponse<List<ReceiverLog>>(
                null, validFilter.PageNumber, validFilter.PageSize, 0));
        }
        var query = new Dictionary<String, String>(){{"key",key},{"value",value}}; ;
        var pageData = await _service.ListFilter(query, collection, validFilter);
        return Ok(new PagedResponse<List<ReceiverLog>>(
            pageData, validFilter.PageNumber, validFilter.PageSize, pageData.Count));
    }
    
}