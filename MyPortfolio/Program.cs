using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL;
using MyPortfolio.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyPortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyPortfolioDb"))); // Veritaban� ba�lant�s�n� ekler

builder.Services.AddScoped<EfSeedData>(); // Seed data s�n�f�n� ekler

builder.Services.AddControllersWithViews(); // MVC servisini ekler

builder.Services.AddAuthentication("AdminCookieAuthentication") // Kimlik do�rulama �emas� ismini belirleme
    .AddCookie("AdminCookieAuthentication", options => // Cookie tabanl� kimlik do�rulama i�in yap�land�rmalar
    {
        options.LoginPath = "/Admin/Index"; // Giri� yap�lmas� gereken sayfa
        options.AccessDeniedPath = "/Admin/Index"; // Eri�im reddedildi�inde y�nlendirilecek sayfa
        options.Cookie.Name = "MyPortfolioAdminAuth"; // Cookie'nin ismi
        options.Cookie.HttpOnly = true; // Cookie'nin JavaScript taraf�ndan eri�ilemez olmas�n� sa�lar
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookie'nin yaln�zca HTTPS �zerinden g�nderilmesini zorunlu k�lar
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider; // Servis sa�lay�c�s�n� al�r
    var myPortfolioContext = services.GetRequiredService<MyPortfolioContext>(); // Veritaban� ba�lant�s�n� al�r
    var efSeedData = new EfSeedData(myPortfolioContext); // Seed data s�n�f�n� olu�turur
    efSeedData.CreateAdminUser();  // Admin kullan�c�y� olu�turur ve veritaban�na kaydeder
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e y�nlendirir
app.UseStaticFiles(); // wwwroot klas�r�ndeki dosyalar�n istemcilere sunulmas�n� sa�lar

app.UseRouting(); // Y�nlendirme i�lemlerini ba�lat�r

app.UseAuthentication(); // Kimlik do�rulama i�lemlerini ba�lat�r
app.UseAuthorization(); // Yetkilendirme i�lemlerini ba�lat�r

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
