using PetPlatform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApplicationDbContext>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();