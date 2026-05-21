using ConcurrentJobProcessor;
using ConcurrentJobProcessor.Endpoints.Extensions;
using ConcurrentJobProcessor.Queues;
using ConcurrentJobProcessor.Workers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<ProductsQueue>();
builder.Services.AddScoped<IWorker, AddProductsWorker>();
builder.Services.AddHostedService<BackgroundWorker>();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
