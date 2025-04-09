using Microsoft.EntityFrameworkCore;
using TWTodos.Contexts;
using TWTodos.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL; // Adicionado para suporte a PostgreSQL

var builder = WebApplication.CreateBuilder(args);

// Carrega configurações do appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configura o DbContext com a connection string para PostgreSQL (Neon)
builder.Services.AddDbContext<TWTodosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registra o TodoService como um serviço injetável
builder.Services.AddScoped<ITodoService, TodoService>();

// Registra controladores com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();
