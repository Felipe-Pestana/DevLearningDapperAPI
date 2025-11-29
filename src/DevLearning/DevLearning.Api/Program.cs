using DevLearning.Api.Data;
using DevLearning.Api.Repositories;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services;
using DevLearning.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddSingleton<CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
