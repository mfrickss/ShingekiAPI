using Microsoft.EntityFrameworkCore;
using MinimalApi;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=titans.db"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/titans/{id}", async (int id, AppDbContext db) =>
{
    var titan = await db.tabelaTitans.FindAsync(id);
    return titan is not null ? Results.Ok(titan) : Results.NotFound("Titã não encontrado!");
});

app.MapPost("/titans", async (Titan titan, AppDbContext db) =>
{
    db.tabelaTitans.Add(titan);
    await db.SaveChangesAsync();
    return Results.Created($"/titans/{titan.id}", titan);
});

app.MapPut("/titans/{id}", async (int id, Titan updatedTitan, AppDbContext db) =>
{
    var existing = await db.tabelaTitans.FindAsync(id);
    if (existing == null)
        return Results.NotFound("Titã não encontrado!");

    existing.nome = updatedTitan.nome;
    existing.portador = updatedTitan.portador;
    existing.idadePortador = updatedTitan.idadePortador;

    await db.SaveChangesAsync();
    return Results.Ok(existing);
});

app.MapDelete("/titans/{id}", async (int id, AppDbContext db) =>
{
    var titan = await db.tabelaTitans.FindAsync(id);
    if (titan == null)
        return Results.NotFound("Titã não encontrado!");

    db.tabelaTitans.Remove(titan);
    await db.SaveChangesAsync();
    return Results.Ok("Titã deletado com sucesso!");
});

app.MapGet("/titans", (AppDbContext db) =>
{
    var titans = db.tabelaTitans.ToList();
    return Results.Ok(titans);
});

app.Run();




















































// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
