using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = AppRoles.CommitteeChair)]
    public class ExecutiveSummaryController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;

        public ExecutiveSummaryController(
            IMonthlyReportService reportService,
            IRecommendationService recommendationService)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "الملخص التنفيذي للرفع القيادي";
            ViewData["EntityHeaderSubtitle"] = "صفحة واحدة موجزة — للرفع لسعادة النائب ومعالي القائد";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-bar-graph";
            ViewData["ThemeColor"] = "#073B49";
            ViewData["Title"] = "الملخص التنفيذي";

            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();

            // التوصيات المُدرجة في الملخص التنفيذي
            var execRecs = allRecs
                .Where(r => r.Status == RecommendationStatus.Approved && r.IncludeInExecutiveSummary)
                .Take(5)
                .ToList();

            // قرارات الدعم المطلوبة (تحتاج قرار سعادة النائب)
            var supportDecisions = allRecs
                .Where(r => r.Status == RecommendationStatus.Approved
                         && r.IncludeInExecutiveSummary
                         && r.Escalation != null
                         && r.Escalation.Contains("النائب"))
                .ToList();

            ViewBag.TotalReports = allReports.Count;
            ViewBag.ApprovedReports = allReports.Count(r => r.Status == ReportStatus.Approved);
            ViewBag.AvgReadiness = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.ReadinessScore), 0) : 0;
            ViewBag.AvgAdoption = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.AdoptionScore), 0) : 0;
            ViewBag.TopRecommendations = execRecs;
            ViewBag.SupportDecisions = supportDecisions;
            ViewBag.GeneratedDate = DateTime.Now;

            return View();
        }
    }
}