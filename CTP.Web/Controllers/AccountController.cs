using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CTP.Infrastructure.Data; // مسار قاعدة البيانات الجديد

namespace CTP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // قمنا بحقن DbContext مباشرة هنا مؤقتاً لتسهيل الاختبار
        // مستقبلاً في المعمارية النظيفة، سننقل هذا المنطق إلى طبقة Application
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var constantDelay = Task.Delay(300);

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "يرجى إدخال اسم المستخدم وكلمة المرور";
                await constantDelay;
                return View();
            }

            // البحث عن المستخدم في قاعدة بيانات CTP مع أدواره
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Organization)
                .FirstOrDefaultAsync(u => u.Username == username);

            // التحقق المؤقت من كلمة المرور (لأغراض الاختبار، سنضيف التشفير لاحقاً)
            if (user == null || user.PasswordHash != password)
            {
                ViewBag.Error = "اسم المستخدم أو كلمة المرور غير صحيحة";
                await constantDelay;
                return View();
            }

            if (!user.IsActive)
            {
                ViewBag.Error = "الحساب غير مفعل. يرجى التواصل مع مسؤول النظام.";
                await constantDelay;
                return View();
            }

            // تحديث بيانات الدخول
            user.LastLogin = DateTime.Now;
            user.FailedLoginAttempts = 0;
            await _context.SaveChangesAsync();

            await constantDelay;
            return await SignInUserAsync(user, rememberMe, returnUrl);
        }

        private async Task<IActionResult> SignInUserAsync(CTP.Domain.Entities.User user, bool rememberMe, string? returnUrl)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.FullName)
            };

            // جلب الأدوار الفعالة للمستخدم
            var activeRoles = user.UserRoles
                .Where(ur => ur.IsActive && (ur.ExpiresDate == null || ur.ExpiresDate > DateTime.Now))
                .ToList();

            foreach (var ur in activeRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, ur.Role.RoleCode));
            }

            if (user.OrganizationEntityId.HasValue)
            {
                claims.Add(new Claim("OrganizationId", user.OrganizationEntityId.Value.ToString()));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home"); // توجيه للشاشة الرئيسية الجديدة
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}