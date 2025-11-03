using AutoMapper;
using CoolLibrary.Application.Mappings;
using CoolLibrary.Domain.Contracts;
using CoolLibrary.Infrastructure.Data;
using CoolLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson(); 

// Configure Database Context
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository Services
builder.Services.AddScoped<IAuthors, AuthorsRepository>();
builder.Services.AddScoped<IBooks, BooksRepository>();
builder.Services.AddScoped<ICustomers, CustomersRepository>();

// Configure AutoMapper 
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<LibraryDbContext>("database");

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CoolLibrary API",
        Version = "v1",
        Description = "A Library Management System API built with Clean Architecture"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoolLibrary API V1");
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Map Health Check endpoint
app.MapHealthChecks("/healthz");

app.MapControllers();

app.Run();
