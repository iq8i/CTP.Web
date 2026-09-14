using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class QualityGateService : IQualityGateService
    {
        public QualityGateResult Check(Recommendation recommendation)
        {
            var report = recommendation.SourceReport;
            var result = new QualityGateResult();

            // ═══════════════════════════════════════════════════════
            // العناصر الـ16
            // ═══════════════════════════════════════════════════════
            result.Items.Add(Item(1, "مصدر التقرير",
                recommendation.MonthlyReportId > 0 ? $"REP-{recommendation.MonthlyReportId}" : "",
                recommendation.MonthlyReportId > 0));

            result.Items.Add(Item(2, "الجهة",
                report?.Organization?.EntityName ?? "",
                !string.IsNullOrWhiteSpace(report?.Organization?.EntityName)));

            result.Items.Add(Item(3, "نوع الجهة",
                report?.Organization?.EntityCode ?? "",
                !string.IsNullOrWhiteSpace(report?.Organization?.EntityCode)));

            result.Items.Add(Item(4, "الفئة المتأثرة",
                report?.AffectedGroups ?? "",
                !string.IsNullOrWhiteSpace(report?.AffectedGroups)));

            result.Items.Add(Item(5, "الفجوة",
                recommendation.GapType,
                !string.IsNullOrWhiteSpace(recommendation.GapType)));

            result.Items.Add(Item(6, "الدليل / المؤشر",
                recommendation.Evidence ?? "",
                !string.IsNullOrWhiteSpace(recommendation.Evidence)));

            result.Items.Add(Item(7, "السبب المحتمل",
                recommendation.Cause ?? "",
                !string.IsNullOrWhiteSpace(recommendation.Cause)));

            result.Items.Add(Item(8, "أثر الفجوة على التبني",
                recommendation.GapEffect ?? "",
                !string.IsNullOrWhiteSpace(recommendation.GapEffect)));

            result.Items.Add(Item(9, "الإجراء المقترح",
                recommendation.SuggestedAction,
                !string.IsNullOrWhiteSpace(recommendation.SuggestedAction)));

            result.Items.Add(Item(10, "مالك الإجراء",
                recommendation.ActionOwner,
                !string.IsNullOrWhiteSpace(recommendation.ActionOwner)));

            result.Items.Add(Item(11, "المدة",
                recommendation.Duration,
                !string.IsNullOrWhiteSpace(recommendation.Duration)));

            result.Items.Add(Item(12, "مؤشر النجاح",
                recommendation.SuccessIndicator,
                !string.IsNullOrWhiteSpace(recommendation.SuccessIndicator)));

            result.Items.Add(Item(13, "طريقة قياس الأثر",
                recommendation.ImpactMeasure ?? "",
                !string.IsNullOrWhiteSpace(recommendation.ImpactMeasure)));

            result.Items.Add(Item(14, "قرار الدعم المطلوب",
                recommendation.SupportDecision ?? "",
                !string.IsNullOrWhiteSpace(recommendation.SupportDecision)));

            result.Items.Add(Item(15, "مستوى الرفع",
                recommendation.Escalation ?? "",
                !string.IsNullOrWhiteSpace(recommendation.Escalation)));

            result.Items.Add(Item(16, "حالة الاعتماد",
                recommendation.Status.ToString(),
                true)); // دائمًا موجود

            return result;
        }

        private static QualityGateItem Item(int n, string label, string value, bool passed)
            => new QualityGateItem
            {
                Number = n,
                Label = label,
                Value = string.IsNullOrWhiteSpace(value) ? "—" : value,
                IsPassed = passed
            };
    }
}