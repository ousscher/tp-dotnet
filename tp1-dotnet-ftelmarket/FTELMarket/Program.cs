using FTELMarket.Infrastructure;
using Microsoft.EntityFrameworkCore;

const string FrontCorsPolicy = "FtelMarketFront";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<FtelMarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FtelMarket")));

// Le front tourne sur une autre origine : le navigateur bloque ses appels sans cette autorisation.
builder.Services.AddCors(options =>
    options.AddPolicy(FrontCorsPolicy, policy => policy
        .WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    // En dev le front appelle le profil http (5082) : une redirection casserait le CORS.
    app.UseHttpsRedirection();
}

app.UseCors(FrontCorsPolicy);

app.MapControllers();

app.Run();
