using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with a connection string
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();

// Enable Swagger UI (the interactive documentation interface)
app.UseSwaggerUI(options =>
{
    // Set the default URL for Swagger UI
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = string.Empty; // Set Swagger UI as the default page
});

app.UseRouting();

// Map controllers
app.MapControllers();

app.Run();
