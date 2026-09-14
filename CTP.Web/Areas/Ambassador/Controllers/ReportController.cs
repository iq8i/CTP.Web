using System.Security.Claims;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;
using CTP.Domain.Enums;
using CTP.Web.Areas.Ambassador.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTP.Application.Helpers;

namespace CTP.Web.Areas.Ambassador.Controllers
{
    [Area("Ambassador")]
    [Authorize(Roles = "AMBASSADOR")]
    public class ReportController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IReportQualityService _qualityService;

        public ReportController(
            IMonthlyReportService reportService,
            IReportQualityService qualityService)
        {
            _reportService = reportService;
            _qualityService = qualityService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["EntityTitle"] = "التقرير الشهري";
            ViewData["EntityHeaderSubtitle"] = "نموذج التقرير الشهري الموحد للتغيير والتحول";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-text";
            return View(new CreateMonthlyReportViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMonthlyReportViewModel model)
        {
            // ─── 1. فحص ModelState التقليدي ───
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "يرجى استكمال الحقول الإلزامية.";
                return View(model);
            }

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orgIdStr = User.FindFirst("OrganizationId")?.Value;

            if (!int.TryParse(userIdStr, out int preparerId) || !int.TryParse(orgIdStr, out int orgId))
            {
                TempData["Error"] = "انتهت صلاحية الجلسة، يرجى إعادة تسجيل الدخول.";
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            // ─── 2. بناء الكيان ───
            int readiness = (model.AdkarAwareness + model.AdkarDesire + model.AdkarKnowledge) / 3;
            int adoption = (model.AdkarAbility + model.AdkarReinforcement) / 2;

            var report = new MonthlyReport
            {
                Year = model.Year,
                Month = model.Month,
                PreparerId = preparerId,
                OrganizationEntityId = orgId,
                ChangeName = model.ChangeName,
                ChangeType = model.ChangeType,
                CurrentStage = model.CurrentStage,
                AffectedGroups = model.AffectedGroups,
                ImpactScope = model.ImpactScope,
                MostInNeedGroup = model.MostInNeedGroup,
                ActivityCompletionRate = model.ActivityCompletionRate,
                ChangeSummary = model.ChangeSummary,
                WhyImportant = model.WhyImportant,
                WhatWillChange = model.WhatWillChange,
                WhatWillNotChange = model.WhatWillNotChange,
                ExecutedActivities = model.ExecutedActivities,
                AdkarAwareness = model.AdkarAwareness,
                AdkarDesire = model.AdkarDesire,
                AdkarKnowledge = model.AdkarKnowledge,
                AdkarAbility = model.AdkarAbility,
                AdkarReinforcement = model.AdkarReinforcement,
                ReadinessScore = readiness,
                AdoptionScore = adoption,
                Obstacles = model.Obstacles,
                AdoptionBarriers = model.AdoptionBarriers,
                RequiredSupport = model.RequiredSupport,
                Risks = model.Risks,
                ImprovementOpportunities = model.ImprovementOpportunities,
                SuccessStories = model.SuccessStories,
                Status = model.ActionType == "Submit" ? ReportStatus.Submitted : ReportStatus.Draft,
                SubmittedDate = model.ActionType == "Submit" ? DateTime.Now : null
            };

            // ─── 3. فحص الجودة ───
            var qualityResult = model.ActionType == "Submit"
                ? _qualityService.CheckForSubmission(report)
                : _qualityService.CheckForDraft(report);

            // ─── 4. إذا Submit وفيه مشاكل → ارجع النموذج مع الأخطاء ───
            if (model.ActionType == "Submit" && !qualityResult.IsValid)
            {
                ViewBag.QualityIssues = qualityResult.Issues;
                ViewBag.QualityScore = qualityResult.QualityPercent;
                TempData["Error"] = $"لا يمكن رفع التقرير — {qualityResult.CriticalIssues.Count} عنصر يحتاج استكمالاً.";
                return View(model);
            }

            // ─── 5. الحفظ ───
            await _reportService.CreateReportAsync(report);

            TempData["Success"] = model.ActionType == "Submit"
                ? $"تم رفع التقرير للاعتماد بنجاح برقم: {report.ReportNumber}"
                : $"تم حفظ المسودة برقم: {report.ReportNumber}";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "تقاريري السابقة";
            ViewData["EntityHeaderSubtitle"] = "سجل التقارير الشهرية ومتابعة الاعتمادات";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["EntityHeaderIcon"] = "bi-clock-history";

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int preparerId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var reports = await _reportService.GetAmbassadorReportsAsync(preparerId);

            ViewBag.ReturnedReportsCount = reports.Count(r => r.Status == ReportStatus.Returned);
            ViewBag.ApprovedReportsCount = reports.Count(r => r.Status == ReportStatus.Approved);

            var viewModel = reports.Select(r => new AmbassadorReportListViewModel
            {
                Id = r.Id,
                ReportNumber = r.ReportNumber,
                MonthYear = $"{r.Month} {r.Year}",
                ChangeName = r.ChangeName,
                CreatedDateFormatted = r.CreatedDate.ToHijri(),
                IsDraft = r.Status == ReportStatus.Draft || r.Status == ReportStatus.Returned,
                StatusName = GetStatusName(r.Status),
                StatusBadgeClass = GetStatusBadgeClass(r.Status)
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (report == null || report.PreparerId.ToString() != userIdStr) return NotFound();

            ViewData["EntityTitle"] = "تفاصيل التقرير";
            ViewData["EntityHeaderSubtitle"] = $"تقرير رقم {report.ReportNumber}";
            ViewData["ThemeColor"] = "#C9A227";

            ViewBag.StatusName = GetStatusName(report.Status);
            ViewBag.StatusBadgeClass = GetStatusBadgeClass(report.Status);

            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (report == null || report.PreparerId.ToString() != userIdStr ||
               (report.Status != ReportStatus.Draft && report.Status != ReportStatus.Returned))
            {
                TempData["Error"] = "لا يمكن تعديل هذا التقرير.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EntityTitle"] = "تعديل واستكمال التقرير";
            ViewData["EntityHeaderSubtitle"] = $"تحديث التقرير رقم {report.ReportNumber}";
            ViewData["ThemeColor"] = "#C9A227";

            var vm = new EditMonthlyReportViewModel
            {
                Id = report.Id,
                ReportNumber = report.ReportNumber,
                Year = report.Year,
                Month = report.Month,
                ChangeName = report.ChangeName,
                ChangeType = report.ChangeType,
                CurrentStage = report.CurrentStage,
                AffectedGroups = report.AffectedGroups,
                ImpactScope = report.ImpactScope ?? "الجهة",
                MostInNeedGroup = report.MostInNeedGroup,
                ActivityCompletionRate = report.ActivityCompletionRate,
                ChangeSummary = report.ChangeSummary,
                WhyImportant = report.WhyImportant,
                WhatWillChange = report.WhatWillChange,
                WhatWillNotChange = report.WhatWillNotChange,
                ExecutedActivities = report.ExecutedActivities,
                AdkarAwareness = report.AdkarAwareness,
                AdkarDesire = report.AdkarDesire,
                AdkarKnowledge = report.AdkarKnowledge,
                AdkarAbility = report.AdkarAbility,
                AdkarReinforcement = report.AdkarReinforcement,
                Obstacles = report.Obstacles,
                AdoptionBarriers = report.AdoptionBarriers,
                RequiredSupport = report.RequiredSupport,
                Risks = report.Risks,
                ImprovementOpportunities = report.ImprovementOpportunities,
                SuccessStories = report.SuccessStories,
                InitialRecommendation = report.InitialRecommendation,
                ApproverNotes = report.ApproverNotes
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMonthlyReportViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var report = await _reportService.GetReportByIdAsync(model.Id);
            if (report == null) return NotFound();

            // تحديث البيانات
            report.Year = model.Year;
            report.Month = model.Month;
            report.ChangeName = model.ChangeName;
            report.ChangeType = model.ChangeType;
            report.CurrentStage = model.CurrentStage;
            report.AffectedGroups = model.AffectedGroups;
            report.ImpactScope = model.ImpactScope;
            report.MostInNeedGroup = model.MostInNeedGroup;
            report.ActivityCompletionRate = model.ActivityCompletionRate;
            report.ChangeSummary = model.ChangeSummary;
            report.WhyImportant = model.WhyImportant;
            report.WhatWillChange = model.WhatWillChange;
            report.WhatWillNotChange = model.WhatWillNotChange;
            report.ExecutedActivities = model.ExecutedActivities;
            report.AdkarAwareness = model.AdkarAwareness;
            report.AdkarDesire = model.AdkarDesire;
            report.AdkarKnowledge = model.AdkarKnowledge;
            report.AdkarAbility = model.AdkarAbility;
            report.AdkarReinforcement = model.AdkarReinforcement;
            report.Obstacles = model.Obstacles;
            report.AdoptionBarriers = model.AdoptionBarriers;
            report.RequiredSupport = model.RequiredSupport;
            report.Risks = model.Risks;
            report.ImprovementOpportunities = model.ImprovementOpportunities;
            report.SuccessStories = model.SuccessStories;
            report.InitialRecommendation = model.InitialRecommendation;

            int readiness = (model.AdkarAwareness + model.AdkarDesire + model.AdkarKnowledge) / 3;
            int adoption = (model.AdkarAbility + model.AdkarReinforcement) / 2;
            report.ReadinessScore = readiness;
            report.AdoptionScore = adoption;

            // ─── فحص الجودة عند Submit ───
            if (model.ActionType == "Submit")
            {
                var qualityResult = _qualityService.CheckForSubmission(report);
                if (!qualityResult.IsValid)
                {
                    ViewBag.QualityIssues = qualityResult.Issues;
                    ViewBag.QualityScore = qualityResult.QualityPercent;
                    TempData["Error"] = $"لا يمكن إعادة الإرسال — {qualityResult.CriticalIssues.Count} عنصر يحتاج استكمالاً.";
                    return View(model);
                }
                report.Status = ReportStatus.Submitted;
                report.SubmittedDate = DateTime.Now;
            }

            await _reportService.UpdateReportAsync(report);

            TempData["Success"] = "تم حفظ وتحديث التقرير بنجاح.";
            return RedirectToAction(nameof(Index));
        }

        private string GetStatusName(ReportStatus status) => status switch
        {
            ReportStatus.Draft => "مسودة",
            ReportStatus.Submitted => "مرفوع",
            ReportStatus.UnderAnalysis => "تحت التحليل",
            ReportStatus.Returned => "معاد للاستكمال",
            ReportStatus.Approved => "معتمد",
            _ => "غير معروف"
        };

        private string GetStatusBadgeClass(ReportStatus status) => status switch
        {
            ReportStatus.Draft => "moda-badge-secondary",
            ReportStatus.Submitted => "moda-badge-info",
            ReportStatus.UnderAnalysis => "moda-badge-gold",
            ReportStatus.Returned => "moda-badge-danger",
            ReportStatus.Approved => "moda-badge-success",
            _ => "moda-badge-secondary"
        };
    }
}