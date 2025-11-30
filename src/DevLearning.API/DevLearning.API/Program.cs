using System.Data.Common;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Repositories;
using DevLearning.API.Services;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDb>();
builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddSingleton<CourseService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
