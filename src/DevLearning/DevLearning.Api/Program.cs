using DevLearning.Api.Data;
using DevLearningAPI.Repositories;
using DevLearningAPI.Repositories.Interfaces;
using DevLearningAPI.Services;
using DevLearningAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ConnectionDB>();

builder.Services.AddScoped<CareerRepository>();
builder.Services.AddScoped<ICareerRepository, CareerRepository>();
builder.Services.AddScoped<ICareerService, CareerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
