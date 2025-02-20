using BH_Estética_VisualStudio.ORM;
using Microsoft.EntityFrameworkCore;
using BH_Estética_VisualStudio.Repositorio;
using BH_Estética_VisualStudio.Agendamento;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar o DbContext se necessário
builder.Services.AddDbContext<BdBhowharmonyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o repositório (UsuarioRepositorio)
builder.Services.AddScoped<UsuarioRepositorio>();
builder.Services.AddScoped<ServicoRepositorio>();
builder.Services.AddScoped<AgendamentoRepositorio>();
builder.Services.AddScoped<RelatorioRepositorio>();
builder.Services.AddScoped<DashboardRepositorio>();

// Adicionar suporte a sessão
builder.Services.AddDistributedMemoryCache(); // Adiciona o cache de memória para sessões
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define o tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Garante que o cookie de sessão seja acessível apenas pelo servidor
    options.Cookie.IsEssential = true; // Marca o cookie como essencial para conformidade com GDPR
});

// Registrar outros serviços, como controllers com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é 30 dias. Pode querer mudar para cenários de produção.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Habilitar o uso de sessões
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
