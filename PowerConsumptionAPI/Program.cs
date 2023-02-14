using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var _connectionString = builder.Configuration.GetConnectionString("sqlConnection");

builder.Services.AddDbContext<RepositoryContext>(
        options => options
            .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
            .EnableDetailedErrors()
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateComputerExistsAttribute>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
