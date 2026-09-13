using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Application.Services;
using CTP.Infrastructure.Data;
using CTP.Infrastructure.Repositories;
using CTP.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/ctp-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
try{
var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. طبقة البيانات
// ==========================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("CTP.Infrastructure"));
});

// ==========================================
// 2. تسجيل المستودعات والخدمات
// ==========================================
builder.Services.AddScoped<ICommitteeRepository, CommitteeRepository>();
builder.Services.AddScoped<ICommitteeService, CommitteeService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IEntityInputRepository, EntityInputRepository>();
builder.Services.AddScoped<IEntityInputService, EntityInputService>();

builder.Services.AddScoped<IMonthlyReportRepository, MonthlyReportRepository>();
builder.Services.AddScoped<IMonthlyReportService, MonthlyReportService>();

builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

builder.Services.AddScoped<IPasswordHasher, CTP.Infrastructure.Security.BCryptPasswordHasher>();
    builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

    // ==========================================
    // 3. MVC
    // ==========================================
    builder.Services.AddControllersWithViews();

// ==========================================
// 4. المصادقة
// ==========================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "CTP_Auth_Cookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StaffAreaPolicy", policy =>
        policy.RequireRole(
            CTP.Domain.Constants.AppRoles.Staff,
            CTP.Domain.Constants.AppRoles.Manager,
            CTP.Domain.Constants.AppRoles.Ambassador,
            CTP.Domain.Constants.AppRoles.CorporateComms));
});

var app = builder.Build();
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("123456", 12);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n\n============================================");
        Console.WriteLine("BCrypt Hash for '123456':");
        Console.WriteLine(hash);
        Console.WriteLine("============================================\n\n");
        Console.ForegroundColor = ConsoleColor.White;
    }

    // ==========================================
    // 5. Middleware Pipeline
    // ==========================================
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}



app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run(); }
catch (Exception ex)
{
    Log.Fatal(ex, "التطبيق فشل في البدء");
}
finally
{
    Log.CloseAndFlush();
}