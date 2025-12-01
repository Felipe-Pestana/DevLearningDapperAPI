using DevLearning.Api.Data;
using DevLearning.Api.Repositories;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services;
using DevLearning.Api.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddSingleton<CourseService>();

builder.Services.AddSingleton<StudentRepository>();
builder.Services.AddSingleton<StudentService>();

builder.Services.AddTransient<ConnectionDB>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
//builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<CategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
