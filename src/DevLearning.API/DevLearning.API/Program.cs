using DevLearning.API.DataBase;
using DevLearning.API.Repositories;
using DevLearning.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDB>();
builder.Services.AddSingleton<CareerRepository>();
builder.Services.AddSingleton<CareerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
