using CTP.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Infrastructure.Repositories;
using CTP.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. تسجيل الخدمات (Dependency Injection)
// ==========================================
builder.Services.AddScoped<ICommitteeRepository, CommitteeRepository>();
builder.Services.AddScoped<ICommitteeService, CommitteeService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEntityInputService, EntityInputService>();
builder.Services.AddScoped<IEntityInputRepository, EntityInputRepository>();
// إضافة خدمات MVC (Controllers & Views)
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

// إخبار النظام بوجود طبقة Infrastructure وربط قاعدة البيانات (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

    // السطر السحري لتجاهل التدقيق الصارم وإجبار تحديث قاعدة البيانات
    options.ConfigureWarnings(warnings =>
        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
}
);
// ==========================================
// 2. إعداد نظام المصادقة والحماية (Authentication & Security)
// ==========================================

// إعداد المصادقة عبر ملفات الارتباط (Cookies)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";          // مسار شاشة تسجيل الدخول
        options.AccessDeniedPath = "/Account/AccessDenied"; // مسار شاشة رفض الوصول
        options.ExpireTimeSpan = TimeSpan.FromHours(8); // مدة الجلسة
        options.SlidingExpiration = true;              // تجديد الجلسة إذا كان المستخدم نشطاً
        options.Cookie.Name = "CTP_Auth_Cookie";       // اسم الـ Cookie
        options.Cookie.HttpOnly = true;                // حماية من هجمات XSS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // يعمل على HTTPS فقط
    });
// إعداد سياسات الوصول (Authorization Policies)
builder.Services.AddAuthorization(options =>
{
    // سياسة "المساحة العامة" تشمل المنسوبين وكل من يعلوهم للوصول للمواد التعليمية وبطاقات التغيير
    options.AddPolicy("StaffAreaPolicy", policy =>
        policy.RequireRole(
            CTP.Domain.Constants.AppRoles.Staff,
            CTP.Domain.Constants.AppRoles.Manager,
            CTP.Domain.Constants.AppRoles.Ambassador,
            CTP.Domain.Constants.AppRoles.CorporateComms
        // يمكن إضافة المزيد من الأدوار التي يحق لها دخول شاشة المنسوبين
        ));
});
builder.Services.AddScoped<IMonthlyReportRepository, MonthlyReportRepository>();
builder.Services.AddScoped<IMonthlyReportService, MonthlyReportService>();// بناء التطبيق
builder.Services.AddScoped<IEntityInputRepository, EntityInputRepository>();
builder.Services.AddScoped<IEntityInputService, EntityInputService>();
var app = builder.Build();

// ==========================================
// 3. إعداد خط أنابيب الطلبات (HTTP Request Pipeline)
// ==========================================

// التعامل مع الأخطاء في بيئة الإنتاج
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // توجيه جميع الطلبات إلى HTTPS
app.UseStaticFiles();      // تفعيل ملفات wwwroot (CSS, JS, Images)

app.UseRouting();          // تفعيل التوجيه

// الترتيب هنا حساس جداً: يجب أن تكون المصادقة قبل الصلاحيات
app.UseAuthentication();   // من أنت؟ (التحقق من الهوية)
app.UseAuthorization();    // ماذا يحق لك أن تفعل؟ (التحقق من الصلاحيات)

// ==========================================
// 4. إعداد مسارات الصفحات (Routing)
// ==========================================
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// 2. التوجيه الافتراضي (للشاشات العامة وتسجيل الدخول)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();