using CTP.Application.Helpers;
using CTP.Application.Interfaces.Services;
using CTP.Application.Services;
using CTP.Domain.Constants;
using CTP.Domain.Entities;
using CTP.Domain.Enums;
using CTP.Web.Areas.Committee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = AppRoles.CommitteeChair + "," + AppRoles.CommitteeMember)]
    public class ChairWorkCenterController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;
        private readonly IAnalysisToolService _analysisTools;
        private readonly IQualityGateService _qualityGate;
        private readonly INotificationService _notificationService;
        private readonly IEntityInputService _inputService;


        public ChairWorkCenterController(
            IMonthlyReportService reportService,
            IRecommendationService recommendationService,
            IAnalysisToolService analysisTools,
            IQualityGateService qualityGate,
            INotificationService notificationService,
            IEntityInputService inputService)  // ← جديد

        {
            _reportService = reportService;
            _recommendationService = recommendationService;
            _analysisTools = analysisTools;
            _qualityGate = qualityGate;
            _notificationService = notificationService;
                _inputService = inputService;   // ← جديد

        }

        [HttpGet]
        public async Task<IActionResult> Index(string tab = "t1", int? reportId = null, int? recommendationId = null)
        {
            ViewData["EntityTitle"] = "مركز أعمال رئيس لجنة شركاء التغيير";
            ViewData["EntityHeaderSubtitle"] = "من التقارير والمدخلات إلى التحليل والتوصيات، ثم الداشبورد والرفع القيادي";
            ViewData["EntityHeaderIcon"] = "bi-shield-check";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "مركز الأعمال";

            var vm = new ChairWorkCenterViewModel { ActiveTab = tab };
            ViewBag.IsChair = User.IsInRole(AppRoles.CommitteeChair);
            ViewBag.IsMember = User.IsInRole(AppRoles.CommitteeMember);

            // ─── Session: التقرير المختار ───
            if (reportId.HasValue)
                HttpContext.Session.SetInt32("ChairSelectedReportId", reportId.Value);

            var selectedId = HttpContext.Session.GetInt32("ChairSelectedReportId");
            if (selectedId.HasValue)
                vm.SelectedReport = await _reportService.GetReportByIdAsync(selectedId.Value);

            // ═══════════════════════════════════════════════════════
            // Tab 1: التقارير الشهرية
            // ═══════════════════════════════════════════════════════
            if (tab == "t1")
                await LoadTab1(vm);

            // ═══════════════════════════════════════════════════════
            // Tab 2: مدخلات الجهات
            // ═══════════════════════════════════════════════════════
            if (tab == "t2")
                await LoadTab2(vm);

           
            // ═══════════════════════════════════════════════════════
            // Tab 3: التحليل
            // ═══════════════════════════════════════════════════════
            if (tab == "t3" && vm.SelectedReport != null)
            {
                vm.Completeness = _analysisTools.CheckCompleteness(vm.SelectedReport);
                vm.Wswnw = _analysisTools.BuildWswnwSummary(vm.SelectedReport);
                vm.Adkar = _analysisTools.AnalyzeAdkar(vm.SelectedReport);
                vm.Pct = _analysisTools.AnalyzePct(vm.SelectedReport);
                vm.AdoptionRisk = _analysisTools.AnalyzeAdoptionRisk(vm.SelectedReport);

                var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
                vm.ParetoBarriers = _analysisTools.AnalyzePareto(allReports);

                // ═══ جديد: هل للتقرير توصية نشطة؟ ═══
                var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
                var existingRec = allRecs.FirstOrDefault(r =>
                    r.MonthlyReportId == vm.SelectedReport.Id
                    && r.Status != RecommendationStatus.Rejected);

                vm.ExistingRecommendationId = existingRec?.Id;
                vm.ExistingRecommendationStatus = existingRec?.Status;
                vm.ExistingRecommendationNumber = existingRec?.RecommendationNumber;

                ViewBag.HasExistingRecommendation = existingRec != null;
            }

            // ═══════════════════════════════════════════════════════
            // Tab 4: التوصيات وبوابة الجودة
            // ═══════════════════════════════════════════════════════
            if (tab == "t4")
                await LoadTab4(vm, recommendationId);


            // ═══════════════════════════════════════════════════════
            // Tab 5: الداشبورد والرفع القيادي
            // ═══════════════════════════════════════════════════════
            if (tab == "t5")
                await LoadTab5(vm);
            return View(vm);

            // ═══════════════════════════════════════════════════════
            // Tab 6: إدارة النماذج
            // ═══════════════════════════════════════════════════════
            if (tab == "t6")
            {
                if (User.IsInRole(AppRoles.CommitteeChair))
                {
                    await LoadTab6(vm);
                }
                else
                {
                    // إعادة توجيه العضو للـ Tab 1
                    return RedirectToAction(nameof(Index), new { tab = "t1" });
                }
            }
        }

        // ═══════════════════════════════════════════════════════
        // Tab 1 loader
        // ═══════════════════════════════════════════════════════
        private async Task LoadTab1(ChairWorkCenterViewModel vm)
        {
            var reports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();

            // ═══════════════════════════════════════════════════════
            // لا نستبعد — نُعلّم فقط
            // ═══════════════════════════════════════════════════════
            var activeRecsByReport = allRecs
                .Where(r => r.Status != RecommendationStatus.Rejected)
                .GroupBy(r => r.MonthlyReportId)
                .ToDictionary(g => g.Key, g => g.First());

            vm.Reports = reports.Select(r =>
            {
                var hasRec = activeRecsByReport.ContainsKey(r.Id);
                var rec = hasRec ? activeRecsByReport[r.Id] : null;

                return new ChairReportListItem
                {
                    Id = r.Id,
                    ReportNumber = r.ReportNumber,
                    EntityName = r.Organization?.EntityName ?? "غير محدد",
                    ChangeName = r.ChangeName,
                    Month = r.Month,
                    Year = r.Year,
                    ReadinessScore = r.ReadinessScore,
                    AdoptionScore = r.AdoptionScore,
                    ActivityCompletionRate = r.ActivityCompletionRate,
                    SubmittedDateFormatted = r.SubmittedDate.ToHijri(),
                    IsComplete = _analysisTools.CheckCompleteness(r).IsComplete,
                    IsLate = IsLate(r),
                    StatusArabic = GetStatusArabic(r.Status),
                    HasActiveRecommendation = hasRec,
                    RecommendationId = rec?.Id,                      // ← جديد
                    RecommendationNumber = rec?.RecommendationNumber,
                    RecommendationStatus = rec?.Status
                };
            })
             .OrderBy(r => r.HasActiveRecommendation)
             .ThenByDescending(r => r.Id)
             .ToList();

            vm.TotalReports = vm.Reports.Count;
            vm.CompleteReports = vm.Reports.Count(r => r.IsComplete);
            vm.IncompleteReports = vm.Reports.Count(r => !r.IsComplete);
            vm.LateReports = vm.Reports.Count(r => r.IsLate);
        }

        // ═══════════════════════════════════════════════════════
        // Tab 4 loader
        // ═══════════════════════════════════════════════════════
        private async Task LoadTab4(ChairWorkCenterViewModel vm, int? recommendationId)
        {
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();

            vm.TotalRecommendations = allRecs.Count;
            vm.PendingRecommendations = allRecs.Count(r =>
                r.Status == RecommendationStatus.ReadyForChair ||
                r.Status == RecommendationStatus.AwaitingSupport);
            vm.ApprovedRecommendations = allRecs.Count(r => r.Status == RecommendationStatus.Approved);
            vm.RejectedRecommendations = allRecs.Count(r => r.Status == RecommendationStatus.Rejected);

            vm.Recommendations = allRecs.Select(r => new ChairRecommendationItem
            {
                Id = r.Id,
                RecommendationNumber = r.RecommendationNumber,
                ReportId = r.MonthlyReportId,
                ReportNumber = r.SourceReport?.ReportNumber ?? "—",
                EntityName = r.SourceReport?.Organization?.EntityName ?? "—",
                GapType = r.GapType,
                SuggestedAction = r.SuggestedAction,
                StatusArabic = GetRecStatusArabic(r.Status),
                StatusBadgeClass = GetRecStatusBadge(r.Status),
                IsPending = r.Status == RecommendationStatus.ReadyForChair
                         || r.Status == RecommendationStatus.AwaitingSupport,
                IsApproved = r.Status == RecommendationStatus.Approved,
                IsRejected = r.Status == RecommendationStatus.Rejected,
                IncludeInInstitutionalReport = r.IncludeInInstitutionalReport,
                IncludeInImpactDashboard = r.IncludeInImpactDashboard,
                IncludeInExecutiveSummary = r.IncludeInExecutiveSummary,
                CreatedDateFormatted = r.CreatedDate.ToHijri(),
                ReviewedDateFormatted = r.ReviewedDate?.ToHijri()
            })
            .OrderByDescending(r => r.IsPending)
            .ThenByDescending(r => r.Id)
            .ToList();

            // ─── اختيار توصية لعرض بوابة الجودة ───
            if (recommendationId.HasValue)
            {
                vm.SelectedRecommendation = allRecs.FirstOrDefault(r => r.Id == recommendationId.Value);
                if (vm.SelectedRecommendation != null)
                    vm.QualityGate = _qualityGate.Check(vm.SelectedRecommendation);
            }
        }

        [HttpPost]
        public IActionResult ClearSelection()
        {
            HttpContext.Session.Remove("ChairSelectedReportId");
            return RedirectToAction(nameof(Index), new { tab = "t1" });
        }

        // ═══════════════════════════════════════════════════════
        // Helpers
        // ═══════════════════════════════════════════════════════
        private static bool IsLate(CTP.Domain.Entities.MonthlyReport r)
        {
            if (!r.SubmittedDate.HasValue) return false;
            return (DateTime.Now - r.SubmittedDate.Value).TotalDays > 30;
        }

        private static string GetStatusArabic(ReportStatus status) => status switch
        {
            ReportStatus.Draft => "مسودة",
            ReportStatus.Submitted => "مرفوع",
            ReportStatus.UnderAnalysis => "تحت التحليل",
            ReportStatus.Returned => "معاد للاستكمال",
            ReportStatus.Approved => "معتمد",
            _ => "غير معروف"
        };

        private static string GetRecStatusArabic(RecommendationStatus status) => status switch
        {
            RecommendationStatus.RequiresCompletion => "تحتاج استكمال",
            RecommendationStatus.AwaitingSupport => "بانتظار قرار الدعم",
            RecommendationStatus.ReadyForChair => "جاهزة لاعتماد الرئيس",
            RecommendationStatus.Approved => "معتمدة",
            RecommendationStatus.Rejected => "مرفوضة",
            RecommendationStatus.SupersededBySovereign => "مستبدلة بتوصية سيادية",
            _ => "غير معروفة"
        };

        private static string GetRecStatusBadge(RecommendationStatus status) => status switch
        {
            RecommendationStatus.ReadyForChair => "bg-warning text-dark",
            RecommendationStatus.AwaitingSupport => "bg-info text-dark",
            RecommendationStatus.Approved => "bg-success",
            RecommendationStatus.Rejected => "bg-danger",
            RecommendationStatus.RequiresCompletion => "bg-secondary",
            RecommendationStatus.SupersededBySovereign => "bg-dark",
            _ => "bg-secondary"
        };

        // ═══════════════════════════════════════════════════════
        // صفحة مراجعة التوصية (اعتماد / رفض)
        // ═══════════════════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> ChairReview(int recommendationId)
        {
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var recommendation = allRecs.FirstOrDefault(r => r.Id == recommendationId);
            ViewBag.IsChair = User.IsInRole(AppRoles.CommitteeChair);
            ViewBag.IsMember = User.IsInRole(AppRoles.CommitteeMember);
            if (recommendation == null)
            {
                TempData["Error"] = "التوصية غير موجودة.";
                return RedirectToAction(nameof(Index), new { tab = "t4" });
            }

            var vm = new ChairWorkCenterViewModel
            {
                ActiveTab = "t4",
                SelectedRecommendation = recommendation,
                QualityGate = _qualityGate.Check(recommendation)
            };

            ViewData["EntityTitle"] = "مراجعة التوصية";
            ViewData["EntityHeaderSubtitle"] = $"التوصية {recommendation.RecommendationNumber} — {recommendation.SourceReport?.Organization?.EntityName}";
            ViewData["EntityHeaderIcon"] = "bi-shield-check";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "مراجعة التوصية";

            return View(vm);
        }
        // ═══════════════════════════════════════════════════════
        // CreateRecommendation — حفظ التوصية الكاملة
        // ═══════════════════════════════════════════════════════
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecommendation(
            int MonthlyReportId,
            string Cause,
            string GapEffect,
            string SuggestedAction,
            string ActionOwner,
            string Duration,
            string SuccessIndicator,
            string ImpactMeasure,
            string? ExpectedImpact,
            string? Evidence,
            bool RequiresSupport)
        {
            var report = await _reportService.GetReportByIdAsync(MonthlyReportId);
            if (report == null)
            {
                TempData["Error"] = "التقرير غير موجود.";
                return RedirectToAction(nameof(Index), new { tab = "t1" });
            }

            // تحديد الفجوة الأضعف
            var weakest = new[]
            {
                ("فجوة وعي", report.AdkarAwareness),
                ("فجوة رغبة", report.AdkarDesire),
                ("فجوة معرفة", report.AdkarKnowledge),
                ("فجوة قدرة", report.AdkarAbility),
                ("فجوة تعزيز", report.AdkarReinforcement)
            }.OrderBy(x => x.Item2).First().Item1;

            var supportDecision = RequiresSupport
                ? "يحتاج قرار دعم قيادي"
                : "لا يتطلب قرار دعم إضافي";

            var escalation = RequiresSupport
    ? "سعادة رئيس فريق عمل القائد"    // ← دائماً لرئيس الفريق أولاً
    : "سعادة رئيس فريق عمل القائد";

            string recNumber = await _recommendationService.CreateFullRecommendationAsync(
                MonthlyReportId,
                weakest,
                Evidence ?? "",
                Cause,
                GapEffect,
                SuggestedAction,
                ActionOwner,
                Duration,
                SuccessIndicator,
                ImpactMeasure,
                ExpectedImpact ?? "",
                supportDecision,
                escalation,
                RequiresSupport);

            TempData["Success"] = $"تم حفظ التوصية ({recNumber}) — بانتظار الاعتماد في سجل التوصيات.";
            return RedirectToAction(nameof(Index), new { tab = "t4" });
        }

        // ═══════════════════════════════════════════════════════
        // Tab 5 loader: الداشبورد المؤسسي
        // ═══════════════════════════════════════════════════════
        private async Task LoadTab5(ChairWorkCenterViewModel vm)
        {
            // جلب جميع التقارير (المعتمدة + تحت التحليل)
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var approvedFromRepo = (await _reportService.GetUnitActiveChangesAsync(1)).ToList();
            var combined = allReports.Union(approvedFromRepo).ToList();

            vm.TotalAnalyzedReports = combined.Count;

            // ─── KPIs مؤسسية ───
            if (combined.Any())
            {
                vm.AvgReadiness = Math.Round(combined.Average(r => (double)r.ReadinessScore), 1);
                vm.AvgAdoption = Math.Round(combined.Average(r => (double)r.AdoptionScore), 1);
                vm.AvgReinforcement = Math.Round(combined.Average(r => (double)r.AdkarReinforcement), 1);

                vm.AvgAdkarAwareness = Math.Round(combined.Average(r => (double)r.AdkarAwareness), 1);
                vm.AvgAdkarDesire = Math.Round(combined.Average(r => (double)r.AdkarDesire), 1);
                vm.AvgAdkarKnowledge = Math.Round(combined.Average(r => (double)r.AdkarKnowledge), 1);
                vm.AvgAdkarAbility = Math.Round(combined.Average(r => (double)r.AdkarAbility), 1);
                vm.AvgAdkarReinforcement = Math.Round(combined.Average(r => (double)r.AdkarReinforcement), 1);
            }

            // ─── توزيع حسب نوع الجهة ───
            var byType = combined
                .Where(r => r.Organization != null)
                .GroupBy(r => r.Organization!.EntityCode.StartsWith("UNIT") ? "وحدة"
                             : r.Organization!.EntityCode.StartsWith("AUTH") ? "هيئة"
                             : r.Organization!.EntityCode.StartsWith("DEPT") ? "إدارة"
                             : "غير محدد")
                .Select(g => new EntityTypeStat
                {
                    EntityType = g.Key,
                    ReportCount = g.Count(),
                    AvgReadiness = Math.Round(g.Average(r => (double)r.ReadinessScore), 1),
                    AvgAdoption = Math.Round(g.Average(r => (double)r.AdoptionScore), 1),
                    ReadinessPercent = (int)Math.Round(g.Average(r => (double)r.ReadinessScore)),
                    AdoptionPercent = (int)Math.Round(g.Average(r => (double)r.AdoptionScore))
                })
                .OrderByDescending(x => x.ReportCount)
                .ToList();

            vm.StatsByEntityType = byType;

            // ─── Pareto على المنظومة ───
            vm.ParetoBarriers = _analysisTools.AnalyzePareto(combined);

            // ─── التوصيات المعتمدة ───
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();

            vm.ApprovedRecommendationsList = allRecs
                .Where(r => r.Status == RecommendationStatus.Approved)
                .Select(r => new ChairRecommendationItem
                {
                    Id = r.Id,
                    RecommendationNumber = r.RecommendationNumber,
                    ReportId = r.MonthlyReportId,
                    ReportNumber = r.SourceReport?.ReportNumber ?? "—",
                    EntityName = r.SourceReport?.Organization?.EntityName ?? "—",
                    GapType = r.GapType,
                    SuggestedAction = r.SuggestedAction,
                    StatusArabic = GetRecStatusArabic(r.Status),
                    StatusBadgeClass = GetRecStatusBadge(r.Status),
                    IncludeInInstitutionalReport = r.IncludeInInstitutionalReport,
                    IncludeInImpactDashboard = r.IncludeInImpactDashboard,
                    IncludeInExecutiveSummary = r.IncludeInExecutiveSummary,
                    CreatedDateFormatted = r.CreatedDate.ToHijri(),
                    ReviewedDateFormatted = r.ReviewedDate?.ToHijri()
                })
                .OrderByDescending(r => r.ReviewedDateFormatted)
                .ToList();

            vm.TotalPlacedInInstitutional = allRecs.Count(r => r.Status == RecommendationStatus.Approved && r.IncludeInInstitutionalReport);
            vm.TotalPlacedInImpact = allRecs.Count(r => r.Status == RecommendationStatus.Approved && r.IncludeInImpactDashboard);
            vm.TotalPlacedInExecutive = allRecs.Count(r => r.Status == RecommendationStatus.Approved && r.IncludeInExecutiveSummary);
        }

        // ═══════════════════════════════════════════════════════
        // B.4b: اعتماد / رفض / إدراج
        // ═══════════════════════════════════════════════════════

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppRoles.CommitteeChair)]  // ← جديد
        public async Task<IActionResult> ApproveRecommendation(int recommendationId, string? chairNotes)
        {
            var rec = await _recommendationService.GetRecommendationByReportIdAsync(
                (await GetRecommendationAsync(recommendationId))?.MonthlyReportId ?? 0);

            // نجلب مباشرة من كل التوصيات
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var recommendation = allRecs.FirstOrDefault(r => r.Id == recommendationId);

            if (recommendation == null)
            {
                TempData["Error"] = "التوصية غير موجودة.";
                return RedirectToAction(nameof(Index), new { tab = "t4" });
            }

            // فحص بوابة الجودة (أمان إضافي)
            var gate = _qualityGate.Check(recommendation);
            if (!gate.IsReadyForApproval)
            {
                TempData["Error"] = $"لا يمكن الاعتماد — {gate.MissingItems} عنصر ناقص.";
                return RedirectToAction(nameof(Index), new { tab = "t4", recommendationId });
            }

            var success = await _recommendationService.ApproveForChairAsync(
                recommendation.MonthlyReportId,
                includeInInstitutionalReport: true,
                includeInImpactDashboard: true,
                includeInExecutiveSummary: true,
                chairReviewNotes: chairNotes);

            if (success)
            {
                TempData["Success"] = $"تم اعتماد التوصية {recommendation.RecommendationNumber} بنجاح.";
            }
            else
            {
                TempData["Error"] = "تعذر اعتماد التوصية.";
            }

            return RedirectToAction(nameof(Index), new { tab = "t4", recommendationId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppRoles.CommitteeChair)]  // ← جديد
        public async Task<IActionResult> RejectRecommendation(int recommendationId, string? chairNotes)
        {
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var recommendation = allRecs.FirstOrDefault(r => r.Id == recommendationId);

            if (recommendation == null)
            {
                TempData["Error"] = "التوصية غير موجودة.";
                return RedirectToAction(nameof(Index), new { tab = "t4" });
            }

            var success = await _recommendationService.RejectChairRecommendationAsync(
                recommendation.MonthlyReportId,
                chairNotes);

            if (success)
            {
                // ═══════════════════════════════════════════════════
                // إشعار أعضاء اللجنة بالتوصية المرفوضة
                // ═══════════════════════════════════════════════════
                var committeeMemberIds = await GetCommitteeMemberUserIdsAsync();

                foreach (var memberId in committeeMemberIds)
                {
                    await _notificationService.CreateNotificationAsync(
                        userId: memberId,
                        title: "توصية مرفوضة — تحتاج مراجعة",
                        message: $"رفض رئيس اللجنة التوصية {recommendation.RecommendationNumber}. السبب: {chairNotes ?? "غير محدد"}",
                        url: "/Committee/Workspace/Inbox",
                        iconClass: "bi-x-circle-fill",
                        colorClass: "text-danger");
                }

                TempData["Success"] = $"تم رفض التوصية {recommendation.RecommendationNumber}، وتم إشعار فريق اللجنة.";
            }
            else
            {
                TempData["Error"] = "تعذر رفض التوصية.";
            }

            return RedirectToAction(nameof(Index), new { tab = "t4", recommendationId });
        }

        // Helper: جلب أعضاء اللجنة
        // ═══════════════════════════════════════════════════════
        // Helper: جلب IDs أعضاء لجنة شركاء التغيير
        // ═══════════════════════════════════════════════════════
        private async Task<List<int>> GetCommitteeMemberUserIdsAsync()
        {
            var db = HttpContext.RequestServices
                .GetRequiredService<CTP.Infrastructure.Data.ApplicationDbContext>();

            var committeeRole = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
                .FirstOrDefaultAsync(db.Roles, r => r.RoleCode == AppRoles.CommitteeMember);

            if (committeeRole == null) return new List<int>();

            return await db.UserRoles
    .Where(ur => ur.RoleId == committeeRole.RoleId && ur.IsActive)
    .Select(ur => ur.UserId)
    .ToListAsync();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppRoles.CommitteeChair)]  // ← جديد
        public async Task<IActionResult> TogglePlacement(
    int recommendationId,
    string placement,
    bool enabled,
    string? returnTo = null)
        {
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var recommendation = allRecs.FirstOrDefault(r => r.Id == recommendationId);

            if (recommendation == null)
            {
                TempData["Error"] = "التوصية غير موجودة.";
                return RedirectToAction(nameof(Index), new { tab = "t4" });
            }

            if (recommendation.Status != RecommendationStatus.Approved)
            {
                TempData["Error"] = "لا يمكن الإدراج قبل اعتماد التوصية.";
                return RedirectToAction(nameof(Index), new { tab = "t4", recommendationId });
            }

            var success = await _recommendationService.UpdateRecommendationPlacementAsync(
                recommendation.MonthlyReportId,
                placement,
                enabled);

            if (success)
            {
                var placementName = placement switch
                {
                    "institutional" => "التقرير المؤسسي الشهري",
                    "impact" => "لوحة الأثر المتحقق",
                    "executive" => "الملخص التنفيذي",
                    _ => placement
                };
                TempData["Success"] = enabled
                    ? $"تم إدراج التوصية في {placementName}."
                    : $"تمت إزالة التوصية من {placementName}.";
            }
            else
            {
                TempData["Error"] = "تعذر تحديث الإدراج.";
            }

            // ═══════════════════════════════════════════════════════
            // التوجيه الذكي: لو جاء الطلب من صفحة المراجعة → ارجع لها
            // ═══════════════════════════════════════════════════════
            if (returnTo == "chairReview")
                return RedirectToAction(nameof(ChairReview), new { recommendationId });

            return RedirectToAction(nameof(Index), new { tab = "t4", recommendationId });
        }
        // ═══════════════════════════════════════════════════════
        // Tab 2 loader: مدخلات الجهات
        // ═══════════════════════════════════════════════════════
        private async Task LoadTab2(ChairWorkCenterViewModel vm)
        {
            var inputs = (await _inputService.GetCommitteeInputsAsync()).ToList();

            vm.EntityInputs = inputs.OrderByDescending(i => i.CreatedDate).ToList();
            vm.TotalInputs = inputs.Count;
            vm.PendingInputs = inputs.Count(i => i.Status == InputStatus.Received);

            // التصنيف حسب النوع
            var byType = new Dictionary<string, int>();
            foreach (var input in inputs)
            {
                var typeName = input.Type switch
                {
                    InputType.Challenge => "تحدٍ",
                    InputType.SuccessStory => "قصة نجاح",
                    InputType.Improvement => "فرصة تحسين",
                    InputType.Inquiry => "استفسار",
                    InputType.SupportNeed => "احتياج دعم",
                    InputType.PlatformFeedback => "ملاحظة على المنصة",
                    InputType.AdoptionBarrier => "عائق تبني",
                    _ => "أخرى"
                };
                byType[typeName] = byType.GetValueOrDefault(typeName) + 1;
            }
            vm.InputsByType = byType;
        }
        // ─── Tab 5: الداشبورد المؤسسي ───

        public int TotalAnalyzedReports { get; set; }
        public double AvgReadiness { get; set; }
        public double AvgAdoption { get; set; }
        public double AvgReinforcement { get; set; }

        // ADKAR على مستوى المنظومة
        public double AvgAdkarAwareness { get; set; }
        public double AvgAdkarDesire { get; set; }
        public double AvgAdkarKnowledge { get; set; }
        public double AvgAdkarAbility { get; set; }
        public double AvgAdkarReinforcement { get; set; }

        // توزيع حسب نوع الجهة
        public List<EntityTypeStat> StatsByEntityType { get; set; } = new();

        // توصيات جاهزة للإدراج
        public List<ChairRecommendationItem> ApprovedRecommendationsList { get; set; } = new();

        // عدد الأثر الحالي
        public int TotalPlacedInInstitutional { get; set; }
        public int TotalPlacedInImpact { get; set; }
        public int TotalPlacedInExecutive { get; set; }

        // Helper — للاستخدام الداخلي فقط
        private async Task<Recommendation?> GetRecommendationAsync(int id)
        {
            var all = await _recommendationService.GetCommitteeRecommendationsAsync();
            return all.FirstOrDefault(r => r.Id == id);
        }
        // ═══════════════════════════════════════════════════════
        // Tab 2: معالجة المدخل (توجيه / دعم / نشر / تصعيد)
        // ═══════════════════════════════════════════════════════
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessInput(int inputId, string actionType)
        {
            var newStatus = actionType switch
            {
                "route" => InputStatus.Routed,
                "support" => InputStatus.SupportIssued,
                "publish" => InputStatus.Published,
                "escalate" => InputStatus.Escalated,
                _ => InputStatus.Received
            };

            var success = await _inputService.UpdateInputStatusAsync(inputId, newStatus);

            var actionName = actionType switch
            {
                "route" => "توجيه للجهة المختصة",
                "support" => "إصدار قرار دعم",
                "publish" => "نشر كقصة نجاح",
                "escalate" => "تصعيد قيادي",
                _ => "معالجة"
            };

            if (success)
            {
                TempData["Success"] = $"تم تنفيذ إجراء «{actionName}» على المدخل بنجاح.";
            }
            else
            {
                TempData["Error"] = "تعذر تحديث حالة المدخل.";
            }

            return RedirectToAction(nameof(Index), new { tab = "t2" });
        }

        // ═══════════════════════════════════════════════════════
        // Tab 6 loader: إدارة النماذج
        // ═══════════════════════════════════════════════════════
        private async Task LoadTab6(ChairWorkCenterViewModel vm)
        {
            // ─── إحصائيات نموذج التقرير الشهري ───
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();

            if (allReports.Any())
            {
                int totalFields = 0;
                int validFields = 0;

                foreach (var report in allReports)
                {
                    var check = _analysisTools.CheckCompleteness(report);
                    totalFields += check.TotalFields;
                    validFields += check.CompletedFields;

                    if (check.IsComplete)
                        vm.ReportsWithFullData++;
                    else
                        vm.ReportsWithMissingData++;
                }

                vm.AvgCompletenessPercent = totalFields > 0
                    ? Math.Round((double)validFields / totalFields * 100, 1)
                    : 0;
            }

            // ─── إحصائيات أنواع المدخلات ───
            var inputs = (await _inputService.GetCommitteeInputsAsync()).ToList();

            var usage = new Dictionary<string, int>();
            foreach (var input in inputs)
            {
                var typeName = input.Type switch
                {
                    InputType.Challenge => "تحدٍ",
                    InputType.SuccessStory => "قصة نجاح",
                    InputType.Improvement => "فرصة تحسين",
                    InputType.Inquiry => "استفسار",
                    InputType.SupportNeed => "احتياج دعم",
                    InputType.PlatformFeedback => "ملاحظة على المنصة",
                    InputType.AdoptionBarrier => "عائق تبني",
                    _ => "أخرى"
                };
                usage[typeName] = usage.GetValueOrDefault(typeName) + 1;
            }
            vm.InputTypesUsage = usage;
        }
    }
}