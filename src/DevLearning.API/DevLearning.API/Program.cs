using DevLearning.API.DataBase;
using DevLearning.API.Repositories;
using DevLearning.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DbConnection>();

var app = builder.Build();

builder.Services.AddSingleton<AuthorRepository>();
builder.Services.AddSingleton<AuthorService>();






// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
