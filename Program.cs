using BH_Est�tica_VisualStudio.ORM;
using Microsoft.EntityFrameworkCore;
using BH_Est�tica_VisualStudio.Repositorio;
using BH_Est�tica_VisualStudio.Agendamento;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar o DbContext se necess�rio
builder.Services.AddDbContext<BdBhowharmonyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o reposit�rio (UsuarioRepositorio)
builder.Services.AddScoped<UsuarioRepositorio>();
builder.Services.AddScoped<ServicoRepositorio>();
builder.Services.AddScoped<AgendamentoRepositorio>();
builder.Services.AddScoped<RelatorioRepositorio>();
builder.Services.AddScoped<DashboardRepositorio>();

// Adicionar suporte a sess�o
builder.Services.AddDistributedMemoryCache(); // Adiciona o cache de mem�ria para sess�es
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define o tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Garante que o cookie de sess�o seja acess�vel apenas pelo servidor
    options.Cookie.IsEssential = true; // Marca o cookie como essencial para conformidade com GDPR
});

// Registrar outros servi�os, como controllers com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o do HSTS � 30 dias. Pode querer mudar para cen�rios de produ��o.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Habilitar o uso de sess�es
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
