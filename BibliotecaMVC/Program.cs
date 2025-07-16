using BibliotecaMVCApp.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/autor", async (BibliotecaDbContext db) => await db.Autores.ToListAsync());
app.MapGet("/livro", async (BibliotecaDbContext db) => await db.Livros.ToListAsync());

app.Run();