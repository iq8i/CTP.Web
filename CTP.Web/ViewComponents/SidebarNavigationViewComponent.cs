using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CTP.Web.Models.Navigation;

namespace CTP.Web.ViewComponents
{
    public class SidebarNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // نجلب دور المستخدم من الجلسة (Cookies)
            var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "STAFF";

            var menu = new List<MenuSection>();

            // 1. القائمة الأساسية (تظهر للجميع)
            var mainSection = new MenuSection { Title = "الرئيسية" };
            mainSection.Items.Add(new MenuItem { Title = "لوحة القياس", IconClass = "bi-speedometer2", Url = "/Home/Index" });
            menu.Add(mainSection);

            // 2. قوائم مخصصة (مثال: تظهر لرئيس اللجنة والقيادة فقط)
            if (role == "COMMITTEE_CHAIR" || role == "LEADER")
            {
                var committeeSection = new MenuSection { Title = "إدارة اللجان" };
                committeeSection.Items.Add(new MenuItem { Title = "مركز الأعمال", IconClass = "bi-bullseye", Url = "#" });
                committeeSection.Items.Add(new MenuItem { Title = "التقارير المرفوعة", IconClass = "bi-file-earmark-text", Url = "#" });
                menu.Add(committeeSection);
            }

            return View("Default", menu);
        }
    }
}