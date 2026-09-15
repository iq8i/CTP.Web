using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.Committee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = AppRoles.CommitteeChair)]
    public class InstitutionalReportController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;
        private readonly IAnalysisToolService _analysisTools;

        public InstitutionalReportController(
            IMonthlyReportService reportService,
            IRecommendationService recommendationService,
            IAnalysisToolService analysisTools)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
            _analysisTools = analysisTools;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? month = null, int? year = null)
        {
            ViewData["EntityTitle"] = "التقرير المؤسسي الشهري";
            ViewData["EntityHeaderSubtitle"] = "تقرير موحد يجمع التوصيات المعتمدة والمؤشرات المؤسسية";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-text";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "التقرير المؤسسي";

            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var approvedFromRepo = (await _reportService.GetUnitActiveChangesAsync(1)).ToList();
            var combined = allReports.Union(approvedFromRepo).ToList();

            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();

            // التوصيات المُدرجة في التقرير المؤسسي فقط
            var institutionalRecs = allRecs
                .Where(r => r.Status == RecommendationStatus.Approved && r.IncludeInInstitutionalReport)
                .ToList();

            // إحصائيات
            var vm = new InstitutionalReportViewModel
            {
                ReportMonth = month ?? DateTime.Now.ToString("MMMM"),
                ReportYear = year ?? DateTime.Now.Year,
                GeneratedDate = DateTime.Now,

                TotalReports = combined.Count,
                ApprovedReports = combined.Count(r => r.Status == ReportStatus.Approved),
                TotalRecommendations = allRecs.Count(r => r.Status == RecommendationStatus.Approved),
                InstitutionalRecommendations = institutionalRecs.Count,

                AvgReadiness = combined.Any() ? Math.Round(combined.Average(r => (double)r.ReadinessScore), 1) : 0,
                AvgAdoption = combined.Any() ? Math.Round(combined.Average(r => (double)r.AdoptionScore), 1) : 0,
                AvgAdkarAwareness = combined.Any() ? Math.Round(combined.Average(r => (double)r.AdkarAwareness), 1) : 0,
                AvgAdkarDesire = combined.Any() ? Math.Round(combined.Average(r => (double)r.AdkarDesire), 1) : 0,
                AvgAdkarKnowledge = combined.Any() ? Math.Round(combined.Average(r => (double)r.AdkarKnowledge), 1) : 0,
                AvgAdkarAbility = combined.Any() ? Math.Round(combined.Average(r => (double)r.AdkarAbility), 1) : 0,
                AvgAdkarReinforcement = combined.Any() ? Math.Round(combined.Average(r => (double)r.AdkarReinforcement), 1) : 0,

                ParetoBarriers = _analysisTools.AnalyzePareto(combined),

                Recommendations = institutionalRecs.Select(r => new ReportRecommendationItem
                {
                    RecommendationNumber = r.RecommendationNumber,
                    EntityName = r.SourceReport?.Organization?.EntityName ?? "—",
                    ReportNumber = r.SourceReport?.ReportNumber ?? "—",
                    GapType = r.GapType,
                    SuggestedAction = r.SuggestedAction,
                    ActionOwner = r.ActionOwner,
                    Duration = r.Duration,
                    SuccessIndicator = r.SuccessIndicator,
                    ExpectedImpact = r.ExpectedImpact ?? "—"
                }).ToList()
            };

            return View(vm);
        }
    }
}