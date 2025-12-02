using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeWaves.Data;
using MQttIoT.Hubs;
using MQttIoT.Services;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔥 ADICIONAR IDENTITY (FALTAVA ISSO!)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// MVC + SignalR
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Serviço MQTT
builder.Services.AddHostedService<MqttService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 Identity precisa disso:
app.UseAuthentication();
app.UseAuthorization();

// SignalR Hub
app.MapHub<AlertaHub>("/alertaHub");

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
