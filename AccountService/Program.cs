using AccountService.Data;
using AccountService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AccountRepository>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
  {
    Title = "Account Service API",
    Version = "v1",
    Description = "API for managing accounts in the Account Service"
  });
  // Add server URL prefix so Swagger "Try it out" works
  c.AddServer(new OpenApiServer { Url = "/account" });
});




var app = builder.Build();



// Migrate & Seed DB
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
  db.Database.Migrate(); // Applies any pending migrations
  DbInitializer.SeedAccounts(db); // Seeds data
}

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
