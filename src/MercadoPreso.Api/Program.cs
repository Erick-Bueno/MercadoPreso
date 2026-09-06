using FastEndpoints;
using MercadoPreso.Api;
using Modules.Catalog.Endpoints;
using Modules.Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder
    .Services
        .AddCatalogInfrastructure(builder.Configuration)
        .AddCatalogEndpoints();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseFastEndpoints();

app.UseExceptionHandler();

app.Run();
