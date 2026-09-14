using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class AnalysisToolService : IAnalysisToolService
    {
        // قائمة الحقول المطلوبة لفحص الاكتمال
        private static readonly (string Key, string Label, Func<MonthlyReport, string?> Getter)[] RequiredFields = new[]
        {
            ("changeName", "اسم التغيير", (Func<MonthlyReport, string?>)(r => r.ChangeName)),
            ("changeType", "نوع التغيير", r => r.ChangeType),
            ("currentStage", "المرحلة الحالية", r => r.CurrentStage),
            ("changeSummary", "ملخص التغيير", r => r.ChangeSummary),
            ("whyImportant", "أهمية التغيير", r => r.WhyImportant),
            ("whatWillChange", "ماذا سيتغير", r => r.WhatWillChange),
            ("whatWillNotChange", "ماذا لن يتغير", r => r.WhatWillNotChange),
            ("executedActivities", "الأنشطة المنفذة", r => r.ExecutedActivities),
            ("obstacles", "العوائق والتحديات", r => r.Obstacles),
            ("requiredSupport", "الدعم المطلوب", r => r.RequiredSupport),
            ("initialRecommendation", "التوصية الأولية", r => r.InitialRecommendation),
            ("affectedGroups", "الفئات المتأثرة", r => r.AffectedGroups),
            ("readinessScore", "مؤشر الجاهزية", r => r.ReadinessScore.ToString()),
            ("adoptionScore", "مؤشر التبني", r => r.AdoptionScore.ToString())
        };

        public CompletenessResult CheckCompleteness(MonthlyReport report)
        {
            var result = new CompletenessResult
            {
                TotalFields = RequiredFields.Length
            };

            foreach (var field in RequiredFields)
            {
                var value = field.Getter(report);
                if (string.IsNullOrWhiteSpace(value) || value == "0")
                {
                    result.MissingFields.Add(field.Label);
                }
                else
                {
                    result.CompletedFields++;
                }
            }

            return result;
        }

        public WswnwResult BuildWswnwSummary(MonthlyReport report)
        {
            // تحديد الفجوة الأضعف
            var dims = new Dictionary<string, int>
            {
                { "فجوة وعي", report.AdkarAwareness },
                { "فجوة رغبة", report.AdkarDesire },
                { "فجوة معرفة", report.AdkarKnowledge },
                { "فجوة قدرة", report.AdkarAbility },
                { "فجوة تعزيز", report.AdkarReinforcement }
            };
            var weakest = dims.OrderBy(x => x.Value).First();

            // الأولوية
            var priority = weakest.Value < 45 ? "عالية"
                         : weakest.Value < 60 ? "متوسطة"
                         : "متابعة";

            // قرار الدعم
            var supportDecision = weakest.Value < 45
                ? "نعم — يُقترح قرار دعم موجه"
                : "لا — ضمن صلاحيات الجهة";

            return new WswnwResult
            {
                What = $"التقرير {report.ReportNumber} من {report.Organization?.EntityName ?? "الجهة"} — " +
                       $"التغيير: {report.ChangeName} في مرحلة {report.CurrentStage}",

                SoWhat = $"استمرار {weakest.Key} بنسبة {weakest.Value}% يبطئ انتقال الفئة المتأثرة " +
                         $"({report.AffectedGroups ?? "غير محددة"}) إلى التطبيق ويؤجل الأثر المستهدف.",

                NowWhat = $"تدخل موجه (توعية + تمكين) للفئة المتأثرة خلال 4 أسابيع، " +
                          $"ثم قياس قبل/بعد في التقرير الشهري التالي.",

                ExpectedImpact = $"رفع مؤشر {weakest.Key.Replace("فجوة ", "")} من {weakest.Value}% إلى " +
                                 $"{Math.Min(100, weakest.Value + 10)}% خلال دورة قياس واحدة",

                Priority = priority,
                Owner = "ممثل الجهة — " + (report.Organization?.EntityName ?? ""),
                SupportDecision = supportDecision
            };
        }

        public AdkarResult AnalyzeAdkar(MonthlyReport report)
        {
            var dims = new Dictionary<string, int>
            {
                { "الوعي (Awareness)", report.AdkarAwareness },
                { "الرغبة (Desire)", report.AdkarDesire },
                { "المعرفة (Knowledge)", report.AdkarKnowledge },
                { "القدرة (Ability)", report.AdkarAbility },
                { "التعزيز (Reinforcement)", report.AdkarReinforcement }
            };

            var barrier = dims.OrderBy(x => x.Value).First();

            return new AdkarResult
            {
                Awareness = report.AdkarAwareness,
                Desire = report.AdkarDesire,
                Knowledge = report.AdkarKnowledge,
                Ability = report.AdkarAbility,
                Reinforcement = report.AdkarReinforcement,
                BarrierName = barrier.Key,
                BarrierValue = barrier.Value
            };
        }

        public PctResult AnalyzePct(MonthlyReport report)
        {
            // Leadership = (Awareness + Desire) / 2
            // ProjectManagement = (Knowledge + Readiness) / 2
            // PeopleManagement = (Ability + Adoption) / 2
            int leadership = (report.AdkarAwareness + report.AdkarDesire) / 2;
            int projectMgmt = (report.AdkarKnowledge + report.ReadinessScore) / 2;
            int peopleMgmt = (report.AdkarAbility + report.AdoptionScore) / 2;

            var dims = new Dictionary<string, (int Value, string Rec)>
            {
                { "القيادة والرعاية", (leadership, "يوصى بتفعيل أكبر لرعاة التغيير.") },
                { "إدارة المشروع", (projectMgmt, "يوصى بضبط الخطة والمواعيد.") },
                { "إدارة الأفراد", (peopleMgmt, "يوصى بتكثيف الدعم المباشر للفئة المتأثرة.") }
            };

            var weakest = dims.OrderBy(x => x.Value.Value).First();

            return new PctResult
            {
                Leadership = leadership,
                ProjectManagement = projectMgmt,
                PeopleManagement = peopleMgmt,
                Weakest = weakest.Key,
                Recommendation = weakest.Value.Rec
            };
        }
        // ═══════════════════════════════════════════════════════
        // B.3b — Pareto
        // ═══════════════════════════════════════════════════════
        public List<ParetoBarrier> AnalyzePareto(List<MonthlyReport> reports, int thresholdPercent = 80)
        {
            if (reports == null || reports.Count == 0)
                return new List<ParetoBarrier>();

            // جمع عوائق التبني (AdoptionBarriers) مع استثناء الفارغة
            var barriers = reports
                .Where(r => !string.IsNullOrWhiteSpace(r.AdoptionBarriers))
                .Select(r => r.AdoptionBarriers!.Trim())
                .ToList();

            if (barriers.Count == 0)
                return new List<ParetoBarrier>();

            // تجميع وتكرار
            var grouped = barriers
                .GroupBy(b => b)
                .Select(g => new { Barrier = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            var total = grouped.Sum(x => x.Count);
            var result = new List<ParetoBarrier>();
            int cumulative = 0;

            foreach (var item in grouped)
            {
                var pct = (int)Math.Round((double)item.Count / total * 100);
                cumulative += pct;

                bool isFocus = cumulative <= thresholdPercent || result.Count == 0;

                result.Add(new ParetoBarrier
                {
                    Barrier = item.Barrier,
                    Count = item.Count,
                    Percentage = pct,
                    CumulativePercentage = cumulative,
                    IsFocusArea = isFocus
                });
            }

            return result;
        }

        // ═══════════════════════════════════════════════════════
        // B.3b — تحليل مخاطر التبني
        // ═══════════════════════════════════════════════════════
        public AdoptionRiskResult AnalyzeAdoptionRisk(MonthlyReport report)
        {
            // تحديد المستوى
            var (level, badge) = report.AdoptionScore switch
            {
                < 50 => ("مرتفع", "bg-danger"),
                < 65 => ("متوسط", "bg-warning text-dark"),
                _ => ("منخفض", "bg-success")
            };

            // العائق الأبرز (AdoptionBarriers أو Obstacles)
            var barrier = !string.IsNullOrWhiteSpace(report.AdoptionBarriers)
                ? report.AdoptionBarriers!
                : !string.IsNullOrWhiteSpace(report.Obstacles)
                    ? report.Obstacles!
                    : "لم يُرفع عائق محدد";

            // السبب المحتمل (مبني على ADKAR)
            var weakest = new[]
            {
                ("الوعي", report.AdkarAwareness, "قصور في وصول رسائل التوعية للفئة المتأثرة"),
                ("الرغبة", report.AdkarDesire, "عدم وضوح المكاسب الشخصية من التغيير"),
                ("المعرفة", report.AdkarKnowledge, "نقص في المحتوى التوضيحي أو التدريب"),
                ("القدرة", report.AdkarAbility, "محدودية الأدوات أو المهارات التطبيقية"),
                ("التعزيز", report.AdkarReinforcement, "عدم ترسيخ الممارسة الجديدة")
            }.OrderBy(x => x.Item2).First();

            // الأثر على النتائج
            var impact = report.AdoptionScore switch
            {
                < 50 => "تأخر الانتقال من التهيئة إلى التطبيق وتأجيل الأثر المستهدف دورة قياس كاملة",
                < 65 => "تباطؤ وتيرة التطبيق وقد يتطلب تدخلاً موجهاً خلال الشهر القادم",
                _ => "الوتيرة مقبولة — متابعة دورية بدون تدخل إضافي"
            };

            return new AdoptionRiskResult
            {
                RiskLevel = level,
                BadgeClass = badge,
                AdoptionScore = report.AdoptionScore,
                TopBarrier = barrier,
                PotentialCause = $"{weakest.Item1} ({weakest.Item2}%) — {weakest.Item3}",
                ImpactOnResults = impact
            };
        }
    }
}