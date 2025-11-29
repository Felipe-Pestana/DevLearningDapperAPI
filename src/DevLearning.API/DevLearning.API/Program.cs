<<<<<<< HEAD
using DevLearning.API.DataBase;
using DevLearning.API.Repositories;
using DevLearning.API.Services;
=======
using DevLearning.API.Repositories;
using DevLearning.API.Repositories.Interfaces;
using DevLearning.API.Services;
using DevLearning.API.Services.Interfaces;
using System.Data.Common;
>>>>>>> e8267d13e485b897a2644cc478d420cfa1043b8c

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

<<<<<<< HEAD
builder.Services.AddSingleton<DbConnection>();
=======
builder.Services.AddScoped<DevLearning.API.DataBase.DbConnection>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
>>>>>>> e8267d13e485b897a2644cc478d420cfa1043b8c

var app = builder.Build();

builder.Services.AddSingleton<AuthorRepository>();
builder.Services.AddSingleton<AuthorService>();






// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
