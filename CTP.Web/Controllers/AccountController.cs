using System.Security.Claims;
using CTP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CTP.Infrastructure.Data;

namespace CTP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AccountController(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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

            // 1. البحث عن المستخدم
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Organization)
                .FirstOrDefaultAsync(u => u.Username == username);

            // 2. التحقق من كلمة المرور (مشفّرة)
            if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
            {
                ViewBag.Error = "اسم المستخدم أو كلمة المرور غير صحيحة";
                await constantDelay;
                return View();
            }

            // 3. التحقق من التنشيط
            if (!user.IsActive)
            {
                ViewBag.Error = "الحساب غير مفعل. يرجى التواصل مع مسؤول النظام.";
                await constantDelay;
                return View();
            }

            // 4. التحقق من القفل
            if (user.LockedUntil.HasValue && user.LockedUntil > DateTime.Now)
            {
                ViewBag.Error = $"الحساب مقفل حتى {user.LockedUntil:yyyy/MM/dd HH:mm}";
                await constantDelay;
                return View();
            }

            // 5. تحديث بيانات الدخول
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
                new Claim(ClaimTypes.GivenName, user.FullName ?? user.Username),
                new Claim("FullName", user.FullName ?? user.Username)
            };

            var activeRoles = user.UserRoles
                .Where(ur => ur.IsActive && (ur.ExpiresDate == null || ur.ExpiresDate > DateTime.Now))
                .ToList();

            foreach (var ur in activeRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, ur.Role.RoleCode));
                claims.Add(new Claim("RoleName", ur.Role.RoleName));
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

            return RedirectToAction("Index", "Home");
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
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}