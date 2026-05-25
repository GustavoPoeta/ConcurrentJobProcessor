using ConcurrentJobProcessor;
using ConcurrentJobProcessor.Channels;
using ConcurrentJobProcessor.Endpoints.Extensions;
using ConcurrentJobProcessor.Hubs;
using ConcurrentJobProcessor.Workers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// not optimal or safe, but as this is just a demo, all origins are allowed
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowCredentials()
              .AllowAnyHeader();
    });
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<ProductsChannel>();

builder.Services.AddHostedService<BackgroundWorker>();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseCors();

app.MapEndpoints(); 

app.MapHub<ProductsHub>("/products/hub", options =>
{
    options.AllowStatefulReconnects = true;
});

app.Run();
