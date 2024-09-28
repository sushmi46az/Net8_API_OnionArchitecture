
using ExtModule.API.Application.Factory;
using ExtModule.API.Application.Interfaces;
using ExtModule.API.Core;
using ExtModule.API.Infrastructure;
using ExtModule.API.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using log4net.Config;
using log4net;
using ExtModule.API;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//configure log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

//Injecting services.
builder.Services.RegisterServices();




builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    //c.IncludeXmlComments(string.Format(@"{0}\ExtModule.API.xml", System.AppDomain.CurrentDomain.BaseDirectory));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ExtModule.API",
    });
});
builder.Services.AddControllers().AddNewtonsoftJson();


var app = builder.Build();


// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionArchitecture");
});

//Exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
