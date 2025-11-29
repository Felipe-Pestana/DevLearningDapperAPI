using DevLearning.API.Repositories;
using DevLearning.API.Repositories.Interfaces;
using DevLearning.API.Services;
using DevLearning.API.Services.Interfaces;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<DevLearning.API.DataBase.DbConnection>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
