var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Pasted code
builder.Services.AddSwaggerGen(); // Adds the Swagger generator

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(); // Enables middleware to serve generated Swagger as a JSON endpoint
    app.UseSwaggerUI(); // Enables middleware to serve the Swagger UI
}

app.UseAuthorization();

app.MapControllers();

app.Run();
