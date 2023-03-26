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

builder.Services.AddSingleton<ILogRepository, LogRepository>();
builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddHostedService<ServerSocket>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy", 
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        );
});


IMapper mapper = MappingConfig.InitializeAutoMapper().CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors();
app.MapControllers();

app.Run();