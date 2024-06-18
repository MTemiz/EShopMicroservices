var builder = WebApplication.CreateBuilder();

// Add services to the container

builder.Services
.AddApplicationServices()
.AddInfrastructureServices(builder.Configuration)
.AddApiServices(builder.Configuration);

var app = builder.Build();

app.AddApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// configure the http request pipeline

app.Run();