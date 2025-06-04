//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });

  // Add Swagger endpoints for downstream services
  c.SwaggerDoc("account", new OpenApiInfo { Title = "Account Service", Version = "v1" });
  c.SwaggerDoc("transaction", new OpenApiInfo { Title = "Transaction Service", Version = "v1" });
  c.SwaggerDoc("notification", new OpenApiInfo { Title = "Notification Service", Version = "v1" });
});

// Add services to the container.
//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot();

// Add YARP proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

//await app.UseOcelot();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/account/swagger/v1/swagger.json", "Account Service");
  c.SwaggerEndpoint("/transaction/swagger/v1/swagger.json", "Transaction Service");
  c.SwaggerEndpoint("/notification/swagger/v1/swagger.json", "Notification Service");
});

// Use routing and the proxy
app.MapReverseProxy();

app.Run();
