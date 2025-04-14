using Microsoft.EntityFrameworkCore;
using ProductControl.Contexts;
using ProductControl.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com a connection string do appsettings.json
builder.Services.AddDbContext<ProductControlContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Injeta o serviço de produtos
builder.Services.AddScoped<IProductService, ProductService>();

// Configura MVC com suporte a views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do ambiente
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"
);

app.Run();
