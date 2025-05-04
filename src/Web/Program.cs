using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});


builder.Services.AddSignalR();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapHub<MessengerHub>("api/hub");

app.MapControllers();

app.Run();