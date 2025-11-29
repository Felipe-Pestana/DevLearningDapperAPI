using DevLearning.Api.Data;
using DevLearning.Api.Repositories;
using DevLearning.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<StudentRepository>();
builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
