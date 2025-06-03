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

app.MapGet("/api/dashboard/{employeeid}/{date}", (int employeeid, DateTime date) => $"{employeeid} {date}");

app.UseHttpsRedirection();

app.Run();
