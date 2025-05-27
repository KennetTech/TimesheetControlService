var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/dashboard", () => "Hello, This is the dashboard api");

app.MapGet("/api/dashboard/timesheet/employee/{id}/{date}", (int id, DateTime date) => $"{id} {date}");

app.MapGet("/api/dashboard/novusdata/employee/{id}/{date}", (int id, DateTime date) => $"{id} {date}");

app.MapGet("/api/dashboard/gpsdata/employee/{id}/{date}", (int id, DateTime date) => $"{id} {date}");

app.UseHttpsRedirection();

app.Run();
