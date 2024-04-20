using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL;
using MyPortfolio.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyPortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyPortfolioDb"))); // Veritabaný baðlantýsýný ekler

builder.Services.AddScoped<EfSeedData>(); // Seed data sýnýfýný ekler

builder.Services.AddControllersWithViews(); // MVC servisini ekler

builder.Services.AddAuthentication("AdminCookieAuthentication") // Kimlik doðrulama þemasý ismini belirleme
    .AddCookie("AdminCookieAuthentication", options => // Cookie tabanlý kimlik doðrulama için yapýlandýrmalar
    {
        options.LoginPath = "/Admin/Index"; // Giriþ yapýlmasý gereken sayfa
        options.AccessDeniedPath = "/Admin/Index"; // Eriþim reddedildiðinde yönlendirilecek sayfa
        options.Cookie.Name = "MyPortfolioAdminAuth"; // Cookie'nin ismi
        options.Cookie.HttpOnly = true; // Cookie'nin JavaScript tarafýndan eriþilemez olmasýný saðlar
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookie'nin yalnýzca HTTPS üzerinden gönderilmesini zorunlu kýlar
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider; // Servis saðlayýcýsýný alýr
    var myPortfolioContext = services.GetRequiredService<MyPortfolioContext>(); // Veritabaný baðlantýsýný alýr
    var efSeedData = new EfSeedData(myPortfolioContext); // Seed data sýnýfýný oluþturur
    efSeedData.CreateAdminUser();  // Admin kullanýcýyý oluþturur ve veritabanýna kaydeder
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e yönlendirir
app.UseStaticFiles(); // wwwroot klasöründeki dosyalarýn istemcilere sunulmasýný saðlar

app.UseRouting(); // Yönlendirme iþlemlerini baþlatýr

app.UseAuthentication(); // Kimlik doðrulama iþlemlerini baþlatýr
app.UseAuthorization(); // Yetkilendirme iþlemlerini baþlatýr

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
