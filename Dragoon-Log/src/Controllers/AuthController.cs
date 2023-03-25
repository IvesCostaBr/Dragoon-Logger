using System.ComponentModel.DataAnnotations;
using Dragoon_Log.DTO;
using Dragoon_Log.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dragoon_Log.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }   
    
    [HttpPost(Name="")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<ResponseCreateAuth> Create(
        [Required][FromBody]RegisterClient data)
    {
        return _service.Create(data);
    }
    
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<List<Client>> List()
    {
        return _service.List();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<bool> Remove(
        [FromRoute]String id)
    {
        return _service.Delete(id);
    }
    
    // [HttpDelete("delete/all")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<bool> DeleteAll()
    // {
    //     return await _service.DeleteAll();
    // }
}