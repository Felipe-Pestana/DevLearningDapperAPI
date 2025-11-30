using API.Database;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Database factory
builder.Services.AddSingleton<DbConnectionFactory>();

// REPOSITORIES
builder.Services.AddSingleton<ICareerRepository, CareerRepository>();

// SERVICES
builder.Services.AddSingleton<ICareerService, CareerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();