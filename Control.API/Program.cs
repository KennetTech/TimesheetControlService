using Control.API;
using Control.API.Models;
using Microsoft.AspNetCore.SignalR;
using Control.API.Kafka;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
Console.WriteLine("signalR up");
builder.Services.AddHostedService<KafkaNovusConsumer>();
Console.WriteLine("hosted service up");
builder.Services.AddDbContext<ControldataDB>(options => options.UseInMemoryDatabase("entries"));
Console.WriteLine("inmemory");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:8080", "http://localhost:3000") // Tilpas dette til din frontend's URL!
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapHub<ControlHub>("/myhub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/kafka/test", async (TimesheetTest timesheet, IHubContext<ControlHub> hubContext) =>
{
    await hubContext.Clients.All.SendAsync("ReceiveDashboardEntry", timesheet);
    Console.WriteLine("Api for DashboardEntry called");
});

app.Run();
