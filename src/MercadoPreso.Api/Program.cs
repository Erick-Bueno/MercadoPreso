using Common.Infrastructure;
using FastEndpoints;
using MercadoPreso.Api;
using Modules.Catalog.Application;
using Modules.Catalog.Endpoints;
using Modules.Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddFastEndpoints(options =>
{
    options.Assemblies = [
        typeof(CatalogGroup).Assembly
    ];
});
builder
    .Services
        .AddCatalogInfrastructure(builder.Configuration);
builder
    .Services
        .AddCommonInfrastructure(builder.Configuration);
builder.Services.AddCatalogApplication();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();


app.UseFastEndpoints(
    options =>
    {
        options.Versioning.Prefix = "v";
        options.Versioning.DefaultVersion = 1;
        options.Versioning.PrependToRoute = true;
        options.Endpoints.RoutePrefix = "api";
    }
);

app.UseExceptionHandler();

app.Run();
