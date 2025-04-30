using ProductApp.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Enable API controllers
builder.Services.AddSingleton<IProductService, ProductService>(); // Register your service
builder.Services.AddEndpointsApiExplorer(); // For Swagger/OpenAPI
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Serve static files (i.e., files in wwwroot)
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Enable attribute routing (e.g. [Route("api/products")])

app.Run();
