using mashwar.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
#region Conect_To_Database
builder.Services.AddDbContext<AppDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
    );
#endregion
#region Language
builder.Services.AddLocalization(option=>option.ResourcesPath= "Resourses");
builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var SuportedLanguage = new[]
    { new CultureInfo("en"),
     new CultureInfo("ar")
    };
    option.DefaultRequestCulture = new RequestCulture("en");
    option.SupportedCultures = SuportedLanguage;
    option.SupportedUICultures = SuportedLanguage;
}
);
#endregion
#region Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    Options =>
    {
        Options.Password.RequiredLength = 8;
        Options.Password.RequireNonAlphanumeric = true;
         Options.Password.RequireUppercase = true;
        Options.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/User/ccessDenied";
    options.Cookie.Name = "cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.LoginPath="/User/Login";
    options.ReturnUrlParameter= CookieAuthenticationDefaults.ReturnUrlParameter;
}
);
#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();

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
var localizationoption = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationoption!.Value);


app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
