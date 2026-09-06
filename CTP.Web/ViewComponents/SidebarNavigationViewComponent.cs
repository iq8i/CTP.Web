using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CTP.Web.Models.Navigation;

namespace CTP.Web.ViewComponents
{
    public class SidebarNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "STAFF";
            var menu = new List<MenuSection>();

            // 1. القائمة الأساسية (للجميع)
            var mainSection = new MenuSection { Title = "الرئيسية" };
            mainSection.Items.Add(new MenuItem { Title = "لوحة القياس", IconClass = "bi-speedometer2", Url = "/Home/Index" });
            mainSection.Items.Add(new MenuItem { Title = "لوحة التغييرات", IconClass = "bi-kanban", Url = "/ChangeBoard/Index" });
            menu.Add(mainSection);

            // 2. مساحة لجنة شركاء التغيير والقيادة
            if (role == "COMMITTEE_CHAIR" || role == "LEADER" || role == "COMMITTEE_MEMBER")
            {
                var committeeSection = new MenuSection { Title = "مركز أعمال اللجنة" };
                committeeSection.Items.Add(new MenuItem { Title = "صندوق الوارد (التحليل)", IconClass = "bi-inboxes", Url = "/Committee/Workspace/Inbox" });
                committeeSection.Items.Add(new MenuItem { Title = "سجل التوصيات", IconClass = "bi-journal-check", Url = "/Committee/Workspace/Recommendations" });
                committeeSection.Items.Add(new MenuItem { Title = "مدخلات الجهات", IconClass = "bi-chat-left-dots", Url = "/Committee/Workspace/Inputs" });

                if (role == "COMMITTEE_CHAIR" || role == "LEADER")
                {
                    committeeSection.Items.Add(new MenuItem { Title = "منصة الاعتماد", IconClass = "bi-shield-check", Url = "/Committee/Workspace/ChairBoard" });
                }

                menu.Add(committeeSection);
            }
            // 3. مساحة سفراء التغيير (التحديث الجديد)
            else if (role == "AMBASSADOR")
            {
                var ambassadorSection = new MenuSection { Title = "إدارة التقارير" };
                ambassadorSection.Items.Add(new MenuItem { Title = "رفع تقرير جديد", IconClass = "bi-file-earmark-plus", Url = "/Ambassador/Report/Create" });
                ambassadorSection.Items.Add(new MenuItem { Title = "تقاريري السابقة", IconClass = "bi-clock-history", Url = "/Ambassador/Report/Index" }); menu.Add(ambassadorSection);
                ambassadorSection.Items.Add(new MenuItem { Title = "مشاركة عاجلة / دعم", IconClass = "bi-chat-left-dots", Url = "/Ambassador/Input/Create" });
            }
            else if (role == CTP.Domain.Constants.AppRoles.UnitLeader)
            {
                var leaderSection = new MenuSection { Title = "مساحة القيادة" };
                leaderSection.Items.Add(new MenuItem { Title = "اعتماد التقارير", IconClass = "bi-building-check", Url = "/UnitLeader/Dashboard/Index" });
                menu.Add(leaderSection);
            }

            return View("Default", menu);
        }
    }
}