using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
  {
    Title = "Notification Service API",
    Version = "v1",
    Description = "API for managing notifications in the Notification Service"
  });
  // Add server URL prefix so Swagger "Try it out" works
  c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = "/notification" });
});

//builder.Services.AddHostedService<FakeEventConsumer>();
builder.Services.AddHostedService<RabbitMqEventConsumer>();

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
