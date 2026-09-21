using Microsoft.EntityFrameworkCore;
using ProyectoX.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using IGDB;
using IGDB.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Auth/Login"; // A dónde redirigir si no está logueado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); //Si está inactivo por 20 minutos, cierra sesión.
        options.SlidingExpiration = true;
    });

var igdb = IGDBClient.CreateWithDefaults(
    builder.Configuration["Igdb:ClientId"], //ClientId
    builder.Configuration["Igdb:ClientSecret"] //Client secret
);
builder.Services.AddSingleton(igdb);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
//builder.Services.AddHttpClient<IgdbTokenService>();
//builder.Services.AddHttpClient<IgdbClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
