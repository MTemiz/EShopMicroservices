using Ordering.Application;
using Ordering.Api;
using Ordering.Inftrastructure;

var builder = WebApplication.CreateBuilder();

// Add services to the container

builder.Services
.AddApplicationServices()
.AddInfrastructureServices(builder.Configuration)
.AddApiServices();

var app = builder.Build();

//app.AddApiServices();

// configure the http request pipeline

app.Run();