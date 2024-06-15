using Ordering.Application;
using Ordering.Api;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder();

// Add services to the container

builder.Services
.AddApplicationServices()
.AddInfrastructureServices(builder.Configuration)
.AddApiServices();

var app = builder.Build();

app.AddApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// configure the http request pipeline

app.Run();