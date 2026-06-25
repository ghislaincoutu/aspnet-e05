using Microsoft.EntityFrameworkCore;
using aspnet05.Data;
using aspnet05.Controllers;
using aspnet05.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("WeatherDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("angular",
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("angular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Weathers.Any())
    {
        context.Weathers.AddRange(
            new Weather { City = "Montréal", Temperature = 20, Condition = "Ensoleillé" },
            new Weather { City = "Québec", Temperature = 10, Condition = "Pluvieux" }
        );
        context.SaveChanges();
    }
}

app.MapControllers();

app.Run();