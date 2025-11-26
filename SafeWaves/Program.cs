using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeWaves.Data;
using SafeWaves.Services;
using SafeWaves.Hubs;
var builder = WebApplication.CreateBuilder(args);
//DbContext
var connectionString =
builder.Configuration.GetConnectionString("LocalDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString)); builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>(); builder.Services.AddControllersWithViews();
//Serviços  

builder.Services.AddScoped<MqttService>();
// SignalR
builder.Services.AddSignalR(); var app = builder.Build();
// Pipeline
if (app.Environment.IsDevelopment()) { app.UseMigrationsEndPoint(); } else { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection(); app.UseRouting(); app.UseAuthorization(); app.MapStaticAssets(); app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets(); app.MapRazorPages().WithStaticAssets();
// Hub do SignalR
app.MapHub<AlertaHub>("/alertahub"); app.Run();