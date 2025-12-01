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

builder.Services.AddControllers();

builder.Services.AddScoped<ConnectionDB>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<Lazy<IStudentService>>(provider =>
    new Lazy<IStudentService>(() => provider.GetRequiredService<IStudentService>())
);

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<Lazy<IAuthorService>>(provider =>
    new Lazy<IAuthorService>(() => provider.GetRequiredService<IAuthorService>())
);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<Lazy<ICategoryService>>(provider =>
    new Lazy<ICategoryService>(() => provider.GetRequiredService<ICategoryService>())
);

builder.Services.AddScoped<ICareerRepository, CareerRepository>();
builder.Services.AddScoped<ICareerService, CareerService>();
builder.Services.AddScoped<Lazy<ICareerService>>(provider =>
    new Lazy<ICareerService>(() => provider.GetRequiredService<ICareerService>())
);

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
