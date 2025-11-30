using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Repositories;
using DevLearning.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<CategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
