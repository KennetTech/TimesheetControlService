using Confluent.Kafka;
using Coravel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Novus.API;
using Novus.API.Models;
using NovusService.Producer;
using NovusService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IkafkaProducer, KafkaProducer>();
builder.Services.AddScheduler();
builder.Services.AddTransient<MyRepeatableTask>();

//var producerConfig = new ProducerConfig();

builder.Services.AddDbContext<NovusdataDbContext>(options =>
options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

var app = builder.Build();
app.Services.UseScheduler(scheduler =>
{

    scheduler.Schedule<MyRepeatableTask>()
        .EveryMinute();

});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/send", async (Novusdatatest novusdatalist) =>
{
    IkafkaProducer producer = new KafkaProducer();
    await producer.ProduceAsync("rute-completed", new Message<string, string>
    {
        Key = novusdatalist.vehicleId,
        Value = JsonConvert.SerializeObject(novusdatalist)
    });
});


app.Run();

