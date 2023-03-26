using AutoMapper;
using Dragoon_Log.Mapping;
using Dragoon_Log.Middlewares;
using Dragoon_Log.Repository;
using Dragoon_Log.Repository.Interfaces;
using Dragoon_Log.Server;
using Dragoon_Log.service;
using Dragoon_Log.service.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First ());
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "API Dragoon", Version = "v1"});
});

IMapper mapper = MappingConfig.InitializeAutoMapper().CreateMapper();

builder.Services.AddSingleton<ILogRepository, LogRepository>();
builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddHostedService<ServerSocket>();
builder.Services.AddSingleton(mapper);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(conf =>
    conf.AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.MapControllers();

app.Run();