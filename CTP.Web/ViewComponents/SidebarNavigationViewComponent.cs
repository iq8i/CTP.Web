using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CTP.Web.Models.Navigation;
using CTP.Domain.Constants;

namespace CTP.Web.ViewComponents
{
    public class SidebarNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var role = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? AppRoles.Staff;

            var menu = new List<MenuSection>();

            // ==========================================
            // 1. القائمة الأساسية (للجميع)
            // ==========================================
            var mainSection = new MenuSection { Title = "الرئيسية" };
            mainSection.Items.Add(new MenuItem
            {
                Title = "لوحة القياس",
                IconClass = "bi-speedometer2",
                Url = "/Home/Index"
            });
            mainSection.Items.Add(new MenuItem
            {
                Title = "لوحة التغييرات",
                IconClass = "bi-kanban",
                Url = "/ChangeBoard/Index"
            });
            menu.Add(mainSection);

            // ==========================================
            // 2. مساحة سفير التغيير
            // ==========================================
            if (role == AppRoles.Ambassador)
            {
                var section = new MenuSection { Title = "إدارة التقارير" };
                section.Items.Add(new MenuItem
                {
                    Title = "رفع تقرير جديد",
                    IconClass = "bi-file-earmark-plus",
                    Url = "/Ambassador/Report/Create"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "تقاريري السابقة",
                    IconClass = "bi-clock-history",
                    Url = "/Ambassador/Report/Index"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "مشاركة عاجلة / دعم",
                    IconClass = "bi-chat-left-dots",
                    Url = "/Ambassador/Input/Create"
                });
                menu.Add(section);
            }

            // ==========================================
            // 3. مساحة قائد الوحدة
            // ==========================================
            else if (role == AppRoles.UnitLeader)
            {
                var section = new MenuSection { Title = "مساحة القيادة" };
                section.Items.Add(new MenuItem
                {
                    Title = "اعتماد التقارير",
                    IconClass = "bi-building-check",
                    Url = "/UnitLeader/Dashboard/Index"
                });
                menu.Add(section);
            }

            // ==========================================
            // 4. مساحة لجنة شركاء التغيير
            // ==========================================
            else if (role == AppRoles.CommitteeChair || role == AppRoles.CommitteeMember)
            {
                var sec = new MenuSection { Title = "مركز أعمال اللجنة" };

                // رابط مركز الأعمال — للرئيس والعضو
                sec.Items.Add(new MenuItem
                {
                    Title = role == AppRoles.CommitteeChair
                        ? "مركز الأعمال الشامل"
                        : "مساحة التحليل",
                    IconClass = "bi-shield-check",
                    Url = "/Committee/ChairWorkCenter/Index"
                });

                // لوحة الأثر — رئيس اللجنة فقط
                if (role == AppRoles.CommitteeChair)
                {
                    sec.Items.Add(new MenuItem
                    {
                        Title = "لوحة الأثر المتحقق",
                        IconClass = "bi-graph-up-arrow",
                        Url = "/Committee/Impact/Index"
                    });

                    sec.Items.Add(new MenuItem
                    {
                        Title = "التقرير المؤسسي الشهري",
                        IconClass = "bi-file-earmark-text",
                        Url = "/Committee/InstitutionalReport/Index"
                    });

                    sec.Items.Add(new MenuItem
                    {
                        Title = "الملخص التنفيذي",
                        IconClass = "bi-file-earmark-bar-graph",
                        Url = "/Committee/ExecutiveSummary/Index"
                    });
                }

                

                menu.Add(sec);
            }

            // ==========================================
            // 5. مساحة معالي القائد (LEADER)
            // ==========================================
            if (role == AppRoles.Leader)
            {
                var section = new MenuSection { Title = "المساحة الاستراتيجية" };
                section.Items.Add(new MenuItem
                {
                    Title = "الموجز التنفيذي",
                    IconClass = "bi-award",
                    Url = "/Leader/Dashboard/Index"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "الأثر المتحقق",
                    IconClass = "bi-graph-up-arrow",
                    Url = "/Leader/Dashboard/Impact"
                });
                menu.Add(section);
            }

            // ==========================================
            // 6. مساحة نائب القائد (DEPUTY_LEADER)
            // ==========================================
            else if (role == AppRoles.DeputyLeader)
            {
                var section = new MenuSection { Title = "مساحة النائب" };
                section.Items.Add(new MenuItem
                {
                    Title = "الملخص التنفيذي",
                    IconClass = "bi-file-earmark-bar-graph",
                    Url = "/DeputyLeader/Dashboard/Index"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "قرارات الدعم",
                    IconClass = "bi-check2-square",
                    Url = "/DeputyLeader/Dashboard/Decisions"
                });
                menu.Add(section);
            }

            // ==========================================
            // 7. مساحة رئيس فريق عمل القائد (CHIEF_OF_STAFF)
            // ==========================================
            else if (role == AppRoles.ChiefOfStaff)
            {
                var section = new MenuSection { Title = "مساحة رئيس الفريق" };
                section.Items.Add(new MenuItem
                {
                    Title = "متابعة الرفع",
                    IconClass = "bi-list-check",
                    Url = "/ChiefOfStaff/Dashboard/Index"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "التقارير المعتمدة",
                    IconClass = "bi-file-earmark-check",
                    Url = "/ChiefOfStaff/Dashboard/ApprovedReports"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "الرفع القيادي",
                    IconClass = "bi-send-check",
                    Url = "/ChiefOfStaff/Dashboard/Escalation"
                });
                menu.Add(section);
            }

            // ==========================================
            // 8. مساحة الاتصال المؤسسي (CORP_COMMS)
            // ==========================================
            else if (role == AppRoles.CorporateComms)
            {
                var section = new MenuSection { Title = "مساحة الاتصال" };
                section.Items.Add(new MenuItem
                {
                    Title = "قوالب الرسائل",
                    IconClass = "bi-chat-quote",
                    Url = "/CorporateComms/Dashboard/Templates"
                });
                section.Items.Add(new MenuItem
                {
                    Title = "إعلانات التغيير",
                    IconClass = "bi-megaphone",
                    Url = "/CorporateComms/Dashboard/Announcements"
                });
                menu.Add(section);
            }

            // ==========================================
            // 9. مساحة المنسوبون (STAFF)
            // ==========================================
            else if (role == AppRoles.Staff || role == AppRoles.Manager)
            {
                var section = new MenuSection { Title = "مساحتي" };
                section.Items.Add(new MenuItem
                {
                    Title = "التغييرات الجارية",
                    IconClass = "bi-kanban",
                    Url = "/ChangeBoard/Index"
                });
                menu.Add(section);
            }

            return View("Default", menu);
        }
    }
}