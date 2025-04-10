using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using PersonAPI.Entities;
using PersonAPI.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<PersonContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION") ?? string.Empty);
});

builder.Services.AddScoped<PersonService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
