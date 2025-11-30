using DevLearning.API.DataBase;
using DevLearning.API.Repositories;
using DevLearning.API.Services;
using DevLearning.API.Repositories.Interfaces;
using DevLearning.API.Services.Interfaces;
using System.Data.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddSingleton<CourseService>();

builder.Services.AddSingleton<ConnectionDB>();
builder.Services.AddSingleton<CareerRepository>();
builder.Services.AddSingleton<CareerService>();
builder.Services.AddSingleton<CareerItemRepository>();
builder.Services.AddSingleton<CareerItemService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<AuthorService>();

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<StudentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
