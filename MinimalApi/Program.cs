using Microsoft.EntityFrameworkCore;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=titans.db"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agora que os serviços foram configurados, podemos criar o app
var app = builder.Build();

// Middlewares
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve arquivos estáticos (wwwroot/index.html, CSS, JS)
app.UseDefaultFiles();
app.UseStaticFiles();

// Rotas da API
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
