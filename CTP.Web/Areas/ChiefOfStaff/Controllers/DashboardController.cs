using CTP.Application.Helpers;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.ChiefOfStaff.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.ChiefOfStaff.Controllers
{
    [Area("ChiefOfStaff")]
    [Authorize(Roles = AppRoles.ChiefOfStaff)]
    public class DashboardController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;
        private readonly ICommitteeService _committeeService;

        public DashboardController(
            IMonthlyReportService reportService,
            IRecommendationService recommendationService,
            ICommitteeService committeeService)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
            _committeeService = committeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "مساحة رئيس فريق عمل القائد";
            ViewData["EntityHeaderSubtitle"] = "متابعة اكتمال الرفع، استقبال الملخصات، وإدارة الرفع القيادي";
            ViewData["EntityHeaderIcon"] = "bi-list-check";
            ViewData["ThemeColor"] = "#2E86A0";
            ViewData["Title"] = "متابعة الرفع";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ApprovedReports()
        {
            ViewData["EntityTitle"] = "التقارير المعتمدة";
            ViewData["EntityHeaderSubtitle"] = "التقارير المعتمدة الواردة من أصحاب السعادة";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-check";
            ViewData["ThemeColor"] = "#2E86A0";
            ViewData["Title"] = "التقارير المعتمدة";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Escalation()
        {
            ViewData["EntityTitle"] = "الرفع القيادي";
            ViewData["EntityHeaderSubtitle"] = "كل التوصيات المعتمدة والمصعدة";
            ViewData["EntityHeaderIcon"] = "bi-send-check";
            ViewData["ThemeColor"] = "#2E86A0";
            ViewData["Title"] = "الرفع القيادي";

            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();

            var vm = new ChiefDashboardViewModel
            {
                ReadyRecommendations = allRecs
                    .Where(r => r.Status == RecommendationStatus.Approved
                             || r.Status == RecommendationStatus.EscalatedToDeputy
                             || r.Status == RecommendationStatus.EscalatedToLeader
                             || r.Status == RecommendationStatus.Rejected)
                    .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                    .Select(r => new ReadyRecommendationItem
                    {
                        Id = r.Id,
                        RecommendationNumber = r.RecommendationNumber,
                        EntityName = r.SourceReport?.Organization?.EntityName ?? "—",
                        SuggestedAction = r.SuggestedAction,
                        ActionOwner = r.ActionOwner,
                        Escalation = r.Escalation ?? "—",
                        Status = r.Status
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Escalate(int recommendationId, string target)
        {
            // target: "deputy" | "leader" | "return"
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var rec = allRecs.FirstOrDefault(r => r.Id == recommendationId);

            if (rec == null)
            {
                TempData["Error"] = "التوصية غير موجودة.";
                return RedirectToAction(nameof(Index));
            }

            if (rec.Status != RecommendationStatus.Approved)
            {
                TempData["Error"] = "لا يمكن تصعيد توصية غير معتمدة.";
                return RedirectToAction(nameof(Index));
            }

            var success = await _recommendationService.UpdateRecommendationEscalationAsync(
                recommendationId, target);

            if (success)
            {
                var msg = target switch
                {
                    "deputy" => "تم تصعيد التوصية لسعادة النائب",
                    "leader" => "تم تصعيد التوصية لمعالي القائد",
                    "return" => "تمت إعادة التوصية للجنة للمراجعة",
                    _ => "تم التحديث"
                };
                TempData["Success"] = msg;
            }
            else
            {
                TempData["Error"] = "تعذر تحديث التوصية.";
            }

            return RedirectToAction(nameof(Index));
        }
        private async Task<ChiefDashboardViewModel> BuildViewModelAsync()
        {
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();

            // ═══ تجميع الجهات ═══
            var entities = allReports
                .Where(r => r.Organization != null)
                .GroupBy(r => new { r.Organization!.Id, r.Organization!.EntityName })
                .Select(g => new EntityStatusItem
                {
                    EntityId = g.Key.Id,
                    EntityName = g.Key.EntityName,
                    TotalReports = g.Count(),
                    ApprovedReports = g.Count(r => r.Status == ReportStatus.Approved),
                    CompletionPercent = g.Count() > 0
                        ? (int)Math.Round((double)g.Count(r => r.Status == ReportStatus.Approved) / g.Count() * 100)
                        : 0,
                    IsComplete = g.All(r => r.Status == ReportStatus.Approved),
                    IsLate = g.Any(r => r.SubmittedDate.HasValue &&
                                      (DateTime.Now - r.SubmittedDate.Value).TotalDays > 30 &&
                                      r.Status != ReportStatus.Approved)
                })
                .OrderBy(e => e.CompletionPercent)
                .ToList();

            var approvedReports = allReports.Where(r => r.Status == ReportStatus.Approved).ToList();
            var approvedRecs = allRecs.Where(r => r.Status == RecommendationStatus.Approved).ToList();

            var vm = new ChiefDashboardViewModel
            {
                TotalEntities = entities.Count,
                EntitiesSubmitted = entities.Count(e => e.TotalReports > 0),
                EntitiesPending = entities.Count(e => !e.IsComplete),
                EntitiesLate = entities.Count(e => e.IsLate),
                TotalReports = allReports.Count,
                ApprovedReports = approvedReports.Count,
                PendingApprovalReports = allReports.Count(r => r.Status == ReportStatus.Submitted),
                CompletionPercent = allReports.Count > 0
                    ? (int)Math.Round((double)approvedReports.Count / allReports.Count * 100)
                    : 0,

                EntitiesStatus = entities.Take(15).ToList(),
                LateEntities = entities.Where(e => e.IsLate).ToList(),

                // ═══ التقارير المعتمدة الحديثة ═══
                RecentApprovedReports = approvedReports
                    .Where(r => r.ApprovedDate.HasValue)
                    .OrderByDescending(r => r.ApprovedDate)
                    .Take(8)
                    .Select(r => new ApprovedReportItem
                    {
                        ReportNumber = r.ReportNumber,
                        EntityName = r.Organization?.EntityName ?? "—",
                        ChangeName = r.ChangeName,
                        ApprovedDate = r.ApprovedDate.ToHijri()
                    })
                    .ToList(),

                // ═══ التوصيات الجاهزة ═══
                ReadyRecommendations = allRecs
    .Where(r => r.Status == RecommendationStatus.Approved)   // ← فقط المعتمدة
    .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
    .Take(3)                                                  // ← فقط 3
    .Select(r => new ReadyRecommendationItem
    {
        Id = r.Id,
        RecommendationNumber = r.RecommendationNumber,
        EntityName = r.SourceReport?.Organization?.EntityName ?? "—",
        SuggestedAction = r.SuggestedAction,
        ActionOwner = r.ActionOwner,
        Escalation = r.Escalation ?? "—",
        Status = r.Status
    })
    .ToList(),
                // ═══ ملخصات رئيس اللجنة ═══
                ChairSummaries = new List<string>
                {
                    $"ملخص {DateTime.Now:MMMM yyyy}: اعتُمد {approvedReports.Count} تقريراً و{approvedRecs.Count} توصية.",
                    $"متوسط الجاهزية: {(allReports.Any() ? Math.Round(allReports.Average(r => (double)r.ReadinessScore), 0) : 0)}%، التبني: {(allReports.Any() ? Math.Round(allReports.Average(r => (double)r.AdoptionScore), 0) : 0)}%.",
                    $"{entities.Count(e => e.IsLate)} جهة متأخرة عن الرفع."
                },

                // ═══ سجل المتابعة ═══
                FollowUpLog = new List<string>
                {
                    $"{DateTime.Now:yyyy/MM/dd} — استلام دفعة التقارير المعتمدة من أصحاب السعادة.",
                    $"{DateTime.Now.AddDays(-1):yyyy/MM/dd} — إحالة النسخ التحليلية لرئيس لجنة شركاء التغيير.",
                    $"{DateTime.Now.AddDays(-2):yyyy/MM/dd} — استلام التقرير الشهري المؤسسي.",
                    $"{DateTime.Now.AddDays(-3):yyyy/MM/dd} — رفع الملخص التنفيذي لسعادة النائب."
                }
            };


            return vm;
        }
        [HttpGet]
        public async Task<IActionResult> Review(int recommendationId)
        {
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var rec = allRecs.FirstOrDefault(r => r.Id == recommendationId);

            if (rec == null)
            {
                TempData["Error"] = "التوصية غير موجودة.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EntityTitle"] = "مراجعة التوصية";
            ViewData["EntityHeaderSubtitle"] = $"التوصية {rec.RecommendationNumber} — {rec.SourceReport?.Organization?.EntityName}";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-check";
            ViewData["ThemeColor"] = "#2E86A0";
            ViewData["Title"] = "مراجعة التوصية";

            var vm = new ChiefRecommendationReviewViewModel
            {
                Recommendation = rec,
                SourceReport = rec.SourceReport,
                PreviousDecisions = new List<PreviousDecisionItem>
        {
            new()
            {
                Action = "رفع التقرير",
                Actor = rec.SourceReport?.Preparer?.FullName ?? "السفير",
                Date = rec.SourceReport?.SubmittedDate?.ToString("yyyy/MM/dd") ?? "—",
                Notes = "تم رفع التقرير للاعتماد",
                BadgeClass = "bg-info"
            },
            new()
            {
                Action = "اعتماد التقرير وإحالته للجنة",
                Actor = "قائد الوحدة",
                Date = rec.SourceReport?.ApprovedDate?.ToString("yyyy/MM/dd") ?? "—",
                Notes = string.IsNullOrWhiteSpace(rec.SourceReport?.ApproverNotes)
                    ? "بدون ملاحظات"
                    : rec.SourceReport.ApproverNotes,
                BadgeClass = "bg-primary"
            },
            new()
            {
                Action = "اعتماد التوصية",
                Actor = "رئيس لجنة شركاء التغيير",
                Date = rec.ReviewedDate?.ToString("yyyy/MM/dd") ?? "—",
                Notes = string.IsNullOrWhiteSpace(rec.ChairReviewNotes)
                    ? "بدون ملاحظات"
                    : rec.ChairReviewNotes,
                BadgeClass = "bg-success"
            }
        }
            };

            return View(vm);
        }
    }
}