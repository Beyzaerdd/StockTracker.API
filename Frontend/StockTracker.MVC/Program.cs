
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;
using System.Globalization;




var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddNToastNotifyToastr(new ToastrOptions
                {
                    ProgressBar = true,
                    PositionClass = ToastPositions.TopRight,
                    CloseButton = true,
                    TimeOut = 5000,
                    ShowDuration = 1000,
                    HideDuration = 1000,
                    ShowEasing = "swing",
                    HideEasing = "linear",
                    ShowMethod = "fadeIn",
                    HideMethod = "fadeOut"
                });









builder.Services.AddHttpClient("StockTracker.APÝ", client => client.BaseAddress = new Uri("http://localhost:7089/api/"));
builder.Services.AddControllersWithViews();
builder
    .Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Dogru.Auth";
        options.LoginPath = "/Auth/LoginUser";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.Cookie.HttpOnly = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<StockTracker.MVC.Areas.Admin.Services.Abstract.IAuthService, StockTracker.MVC.Areas.Admin.Services.Concrete.AuthService>();



builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("ECommerce.MVC")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

builder.Services.AddDistributedMemoryCache();

builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();  // Routing middleware'ý buraya gelmeli.

app.UseAuthentication(); // Authentication middleware'ý




app.UseMiddleware<StockTracker.MVC.Middlewares.TokenExpirationMiddleware>();  // Token expiration kontrolü

// MapAreaControllerRoute'ý burada kullanýn
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" }
);

// Default route için yapýlandýrma
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();  // Uygulamanýn çalýþtýrýlmasý

