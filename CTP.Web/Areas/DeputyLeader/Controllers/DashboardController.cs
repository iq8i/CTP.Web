using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.DeputyLeader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.DeputyLeader.Controllers
{
    [Area("DeputyLeader")]
    [Authorize(Roles = AppRoles.DeputyLeader)]
    public class DashboardController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;
        private readonly IImpactMeasurementService _impactService;

        public DashboardController(
            IMonthlyReportService reportService,
            IRecommendationService recommendationService,
            IImpactMeasurementService impactService)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
            _impactService = impactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "مساحة سعادة النائب";
            ViewData["EntityHeaderSubtitle"] = "الملخص التنفيذي الشهري بعد الاعتماد — دون بيانات خام";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-bar-graph";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "الملخص التنفيذي";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Decisions()
        {
            ViewData["EntityTitle"] = "قرارات الدعم";
            ViewData["EntityHeaderSubtitle"] = "قرارات الدعم المطلوبة من سعادة النائب";
            ViewData["EntityHeaderIcon"] = "bi-check2-square";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "قرارات الدعم";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        private async Task<DeputyDashboardViewModel> BuildViewModelAsync()
        {
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();
            var impactSummary = await _impactService.GetSummaryAsync();

            var approvedRecs = allRecs.Where(r => r.Status == RecommendationStatus.Approved).ToList();
            var supportDecisions = allRecs
     .Where(r => r.Status == RecommendationStatus.EscalatedToDeputy
              || (r.Status == RecommendationStatus.Approved
                  && r.Escalation != null
                  && r.Escalation.Contains("النائب")))
     .ToList();

            // ═══ مؤشرات ═══
            var readiness = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.ReadinessScore), 0) : 0;
            var adoption = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.AdoptionScore), 0) : 0;
            var reinforcement = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.AdkarReinforcement), 0) : 0;

            var vm = new DeputyDashboardViewModel
            {
                TotalReports = allReports.Count,
                ApprovedReports = allReports.Count(r => r.Status == ReportStatus.Approved),
                AvgReadiness = readiness,
                AvgAdoption = adoption,
                ApprovedRecommendations = approvedRecs.Count,
                PendingDecisions = supportDecisions.Count,

                CurrentMonth = DateTime.Now.ToString("MMMM yyyy"),
                MonthlySummary = BuildMonthlySummary(allReports, approvedRecs, impactSummary),

                ReadinessCurrent = readiness,
                AdoptionCurrent = adoption,
                ReinforcementCurrent = reinforcement,

                // ═══ التوصيات المعتمدة ═══
                ApprovedRecommendationsList = approvedRecs
                    .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                    .Take(8)
                    .Select(r => new ApprovedRecommendationItem
                    {
                        RecommendationNumber = r.RecommendationNumber,
                        EntityName = r.SourceReport?.Organization?.EntityName ?? "—",
                        SuggestedAction = r.SuggestedAction,
                        ActionOwner = r.ActionOwner,
                        Duration = r.Duration
                    })
                    .ToList(),

                // ═══ قرارات الدعم ═══
                SupportDecisions = supportDecisions
                    .Take(5)
                    .Select(r => new SupportDecisionItem
                    {
                        Title = r.SuggestedAction,
                        Entity = r.SourceReport?.Organization?.EntityName ?? "—",
                        RequestedBy = r.ActionOwner,
                        ExpectedImpact = r.ExpectedImpact ?? "—"
                    })
                    .ToList(),

                // ═══ الأثر ═══
                ImpactVerifiedCount = impactSummary.VerifiedCount,
                AvgImpactDelta = impactSummary.AvgDelta,
                TopImpacts = impactSummary.AllMeasurements
                    .Where(m => m.Status == "تحقق")
                    .OrderByDescending(m => m.Delta)
                    .Take(4)
                    .Select(m => new ImpactItem
                    {
                        IndicatorName = m.IndicatorName,
                        Before = m.BeforeValue,
                        After = m.AfterValue,
                        Delta = m.Delta
                    })
                    .ToList(),

                // ═══ خطة الشهر القادم ═══
                NextMonthPlan = new List<string>
                {
                    "استكمال مراجعة المادة العلمية واعتمادها ثم التدشين الداخلي.",
                    "بدء تأهيل سفراء التغيير وفق الخطة المعتمدة.",
                    "تشغيل الدورة الأولى للتقارير الشهرية عبر المنصة.",
                    $"متابعة {supportDecisions.Count} قرار دعم مرفوع لسعادة النائب."
                }
            };

            return vm;
        }

        private static string BuildMonthlySummary(
            List<CTP.Domain.Entities.MonthlyReport> reports,
            List<CTP.Domain.Entities.Recommendation> approvedRecs,
            ImpactSummaryResult impact)
        {
            if (!reports.Any())
                return "لم تُستقبل تقارير بعد.";

            var approvedCount = reports.Count(r => r.Status == ReportStatus.Approved);
            var avgReadiness = Math.Round(reports.Average(r => (double)r.ReadinessScore), 0);
            var avgAdoption = Math.Round(reports.Average(r => (double)r.AdoptionScore), 0);

            return $@"اكتمل استلام واعتماد {approvedCount} تقريراً شهرياً موحداً من مختلف الجهات.
بلغت الجاهزية العامة {avgReadiness}% والتبني العام {avgAdoption}% على مستوى المنظومة.
اعتُمدت {approvedRecs.Count} توصية موجهة لسد فجوات التبني، وقيست {impact.TotalMeasurements} مؤشرات أثرية بمتوسط تحسن +{impact.AvgDelta} نقطة.
المرحلة الحالية: مراجعة المادة العلمية واعتمادها، والتحضير جارٍ لتدشين المنصة وتأهيل السفراء.";
        }
    }
}