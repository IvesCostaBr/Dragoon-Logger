using System.Net;
using System.Net.Http.Headers;
using Dragoon_Log.Repository.Interfaces;
using Dragoon_Log.Utils;
using Newtonsoft.Json;

namespace Dragoon_Log.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private IAuthRepository _repo;

    public AuthenticationMiddleware(
        RequestDelegate next,
        IAuthRepository repo)
    {
        _repo = repo;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var password = httpContext.Request.Headers.FirstOrDefault(
            x => x.Key == "SecretKey").Value;
        var clientId = httpContext.Request.Headers.FirstOrDefault(
            x => x.Key == "ClientId").Value;
        var clientSecret = httpContext.Request.Headers.FirstOrDefault(
            x => x.Key == "ClientSecret").Value;
        var result = await _repo.Filter(clientId, clientSecret);
            if (result.Count == 0 && password != Config.PASSWORD)
            {
                httpContext.Response.StatusCode = 401;
                
                var json = JsonConvert.SerializeObject(Responses.Forbiden);
                await httpContext.Response.WriteAsync(json);
                return;
            }
            await _next(httpContext);
    }
}