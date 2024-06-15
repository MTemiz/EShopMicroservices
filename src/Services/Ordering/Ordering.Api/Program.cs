using Ordering.Application;
using Ordering.Inftrastructure;
using Ordering.Api;

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