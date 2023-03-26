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
        var clientId = httpContext.Request.Headers.FirstOrDefault(
            x => x.Key == "Client-Id").Value.FirstOrDefault();
        var clientSecret = httpContext.Request.Headers.FirstOrDefault(
            x => x.Key == "Client-Secret").Value.FirstOrDefault();
        var password = httpContext.Request.Headers.FirstOrDefault(
            x => x.Key == "KEY").Value.FirstOrDefault();
        Console.WriteLine($"Key pass {password}");
        Console.WriteLine($"key expected {Config.PASSWORD}");
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

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthenticationMiddleware(IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
