using System.Text.RegularExpressions;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class ReportQualityService : IReportQualityService
    {
        // ═══════════════════════════════════════════════════════
        // قائمة القيم المُزيفة (Case-insensitive)
        // ═══════════════════════════════════════════════════════
        private static readonly HashSet<string> FakeValues = new(StringComparer.OrdinalIgnoreCase)
        {
            // رموز
            "-", "--", "---", "----", ".", "..", "...", "_", "__", "=", "==",
            "?", "??", "*", "**", "#", "##", "/", "//", "\\", "|", "+",

            // كلمات إنجليزية
            "N/A", "NA", "N.A", "NULL", "NONE", "NO", "TEST", "TBD", "TBA",
            "XXX", "X", "XXX", "YYY", "ZZZ", "AAAA", "FFFF", "DDD",
            "NOTHING", "EMPTY", "BLANK", "NIL", "NAN",

            // كلمات عربية
            "لا يوجد", "لايوجد", "لا شى", "لا شئ", "لا شيء", "لايعرف",
            "غير محدد", "غير معروف", "غير موجود", "غير متوفر", "غير متاح",
            "لا اعرف", "لا أعرف", "مافي", "ما فيه", "مافيه",
            "تجربة", "اختبار", "تجريبي", "test",
            "بدون", "بدون ملاحظات", "بدون تفاصيل", "بدون بيانات",
            "عادي", "طبيعي", "الوضع طبيعي",
            "نفس الشي", "كما هو", "كما هي", "لا جديد", "لا جديد يذكر",
            "لا تعليق", "لا ملاحظات", "لا يحتاج",
            "وهمي", "مؤقت", "لم يتم", "لم ينفذ",
            "أنظر أعلاه", "انظر أعلاه", "مذكور أعلاه"
        };

        // Regex: كشف الحشو المتكرر (مثل "aaaaa" أو "11111" أو ".....")
        private static readonly Regex RepeatedCharsPattern = new(
            @"^(.)\1{4,}$",
            RegexOptions.Compiled);

        // Regex: كشف النصوص العشوائية (لا تحتوي حرف عربي أو إنجليزي)
        private static readonly Regex ContainsLetterPattern = new(
            @"[\u0600-\u06FFa-zA-Z]",
            RegexOptions.Compiled);

        // ═══════════════════════════════════════════════════════
        // قواعد الحقول: (FieldName، Label، MinLength، IsCritical)
        // ═══════════════════════════════════════════════════════
        private static readonly (string Field, string Label, int MinLen, bool Critical)[] TextFieldRules =
        {
            // الحقول الحرجة — يجب أن تكون ممتلئة وقوية
            ("ChangeName",            "اسم التغيير",             5,  true),
            ("ChangeSummary",         "ملخص التغيير",            20, true),
            ("WhyImportant",          "لماذا التغيير مهم",       20, true),
            ("WhatWillChange",        "ماذا سيتغير",             20, true),
            ("WhatWillNotChange",     "ماذا لن يتغير",           15, true),
            ("ExecutedActivities",    "الأنشطة المنفذة",         15, true),
            ("Obstacles",             "العوائق والتحديات",       15, true),
            ("RequiredSupport",       "الدعم المطلوب",           15, true),
            ("InitialRecommendation", "التوصية الأولية",         20, true),
            ("AdoptionBarriers",      "عوائق التبني",            15, true),
            ("AffectedGroups",        "الفئات المتأثرة",         5,  true),
            ("MostInNeedGroup",       "الأكثر احتياجاً للدعم",   5,  true),

            // حقول ثانوية — تحذيرات فقط
            ("Risks",                     "المخاطر",             10, false),
            ("ImprovementOpportunities",  "فرص التحسين",         10, false),
            ("SuccessStories",            "قصص النجاح",          10, false),
            ("ChangeType",                "نوع التغيير",         3,  true),  // لكن من select
            ("CurrentStage",              "المرحلة الحالية",     3,  true),
            ("ImpactScope",               "نطاق الأثر",          3,  true),
        };

        public ReportQualityResult Check(MonthlyReport report)
            => CheckInternal(report, strictMode: false);

        public ReportQualityResult CheckForDraft(MonthlyReport report)
        {
            // Draft: فقط Critical issues تمنع الحفظ
            var result = CheckInternal(report, strictMode: false);
            result.Issues = result.Issues.Where(i => i.Severity == IssueSeverity.Critical).ToList();
            return result;
        }

        public ReportQualityResult CheckForSubmission(MonthlyReport report)
        {
            // Submit: كل شيء حرج
            var result = CheckInternal(report, strictMode: true);
            return result;
        }

        // ═══════════════════════════════════════════════════════
        // التنفيذ الأساسي
        // ═══════════════════════════════════════════════════════
        private ReportQualityResult CheckInternal(MonthlyReport report, bool strictMode)
        {
            var result = new ReportQualityResult();

            // ─── فحص كل حقل نصي ───
            foreach (var rule in TextFieldRules)
            {
                result.TotalFields++;

                var value = GetFieldValue(report, rule.Field);
                var issue = ValidateField(value, rule.Field, rule.Label, rule.MinLen);

                if (issue != null)
                {
                    // لو strictMode، نرفع الحقل الثانوي إلى Critical
                    if (strictMode && !rule.Critical)
                        issue.Severity = IssueSeverity.Critical;

                    result.Issues.Add(issue);
                }
                else
                {
                    result.ValidFields++;
                }
            }

            // ─── فحص المؤشرات الرقمية ───
            CheckNumericMetrics(report, result, strictMode);

            return result;
        }

        private static string? GetFieldValue(MonthlyReport report, string fieldName)
        {
            return fieldName switch
            {
                "ChangeName" => report.ChangeName,
                "ChangeSummary" => report.ChangeSummary,
                "WhyImportant" => report.WhyImportant,
                "WhatWillChange" => report.WhatWillChange,
                "WhatWillNotChange" => report.WhatWillNotChange,
                "ExecutedActivities" => report.ExecutedActivities,
                "Obstacles" => report.Obstacles,
                "RequiredSupport" => report.RequiredSupport,
                "InitialRecommendation" => report.InitialRecommendation,
                "AdoptionBarriers" => report.AdoptionBarriers,
                "AffectedGroups" => report.AffectedGroups,
                "MostInNeedGroup" => report.MostInNeedGroup,
                "Risks" => report.Risks,
                "ImprovementOpportunities" => report.ImprovementOpportunities,
                "SuccessStories" => report.SuccessStories,
                "ChangeType" => report.ChangeType,
                "CurrentStage" => report.CurrentStage,
                "ImpactScope" => report.ImpactScope,
                _ => null
            };
        }

        private static FieldIssue? ValidateField(string? value, string fieldName, string label, int minLength)
        {
            // 1. فارغ
            if (string.IsNullOrWhiteSpace(value))
            {
                return new FieldIssue
                {
                    FieldName = fieldName,
                    FieldLabel = label,
                    Reason = "الحقل فارغ",
                    Severity = IssueSeverity.Critical
                };
            }

            var trimmed = value.Trim();

            // 2. قيمة مُزيفة (من القائمة)
            if (FakeValues.Contains(trimmed))
            {
                return new FieldIssue
                {
                    FieldName = fieldName,
                    FieldLabel = label,
                    Reason = $"قيمة غير مقبولة: \"{trimmed}\" — يرجى كتابة محتوى حقيقي",
                    Severity = IssueSeverity.Critical
                };
            }

            // 3. حشو متكرر (aaaa، 111، .....)
            if (RepeatedCharsPattern.IsMatch(trimmed))
            {
                return new FieldIssue
                {
                    FieldName = fieldName,
                    FieldLabel = label,
                    Reason = "نص مكرر — يرجى كتابة محتوى حقيقي",
                    Severity = IssueSeverity.Critical
                };
            }

            // 4. قصير جداً
            if (trimmed.Length < minLength)
            {
                return new FieldIssue
                {
                    FieldName = fieldName,
                    FieldLabel = label,
                    Reason = $"قصير جداً — الحد الأدنى {minLength} حرف (الحالي {trimmed.Length})",
                    Severity = IssueSeverity.Critical
                };
            }

            // 5. لا يحتوي أي حرف حقيقي (أرقام ورموز فقط)
            if (!ContainsLetterPattern.IsMatch(trimmed))
            {
                return new FieldIssue
                {
                    FieldName = fieldName,
                    FieldLabel = label,
                    Reason = "لا يحتوي محتوى نصي حقيقي (أرقام/رموز فقط)",
                    Severity = IssueSeverity.Critical
                };
            }

            // 6. الحقل يبدأ/ينتهي برمز غريب
            if (Regex.IsMatch(trimmed, @"^[-_=\*#\.]+|[-_=\*#\.]+$"))
            {
                return new FieldIssue
                {
                    FieldName = fieldName,
                    FieldLabel = label,
                    Reason = "الحقل يحتوي على رموز حشو فقط في البداية أو النهاية",
                    Severity = IssueSeverity.Warning
                };
            }

            return null;
        }

        private static void CheckNumericMetrics(MonthlyReport report, ReportQualityResult result, bool strictMode)
        {
            // مؤشرات ADKAR — يجب أن تكون > 0 (على الأقل واحد منها)
            result.TotalFields++;

            var adkarSum = report.AdkarAwareness + report.AdkarDesire
                         + report.AdkarKnowledge + report.AdkarAbility
                         + report.AdkarReinforcement;

            if (adkarSum == 0)
            {
                result.Issues.Add(new FieldIssue
                {
                    FieldName = "AdkarAll",
                    FieldLabel = "مؤشرات ADKAR",
                    Reason = "جميع مؤشرات ADKAR صفر — يجب تعبئتها بصدق",
                    Severity = IssueSeverity.Critical
                });
            }
            else
            {
                result.ValidFields++;
            }

            // مؤشر الجاهزية
            result.TotalFields++;
            if (report.ReadinessScore == 0)
            {
                result.Issues.Add(new FieldIssue
                {
                    FieldName = "ReadinessScore",
                    FieldLabel = "مؤشر الجاهزية",
                    Reason = "مؤشر الجاهزية صفر — يجب تعبئته",
                    Severity = strictMode ? IssueSeverity.Critical : IssueSeverity.Warning
                });
            }
            else
            {
                result.ValidFields++;
            }

            // مؤشر التبني
            result.TotalFields++;
            if (report.AdoptionScore == 0)
            {
                result.Issues.Add(new FieldIssue
                {
                    FieldName = "AdoptionScore",
                    FieldLabel = "مؤشر التبني",
                    Reason = "مؤشر التبني صفر — يجب تعبئته",
                    Severity = strictMode ? IssueSeverity.Critical : IssueSeverity.Warning
                });
            }
            else
            {
                result.ValidFields++;
            }
        }
    }
}