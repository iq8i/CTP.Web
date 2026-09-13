using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPrecisePipelines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "CurrentStage", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "مسودة مشروع الأتمتة", "تعريف", 0, "REP-001", 1, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeType", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "يرجى تفصيل الميزانية المطلوبة", "تهيئة البيئة السحابية", "تقني", 1, 3, 0, "REP-002", 4, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ChangeName", "CurrentStage", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, null, "نظام التذاكر", "تعريف", 1, 3, 0, "REP-003", 1, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActivityCompletionRate", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdoptionScore", "ChangeName", "CurrentStage", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 0, 0, 0, 0, 0, "تطبيق الحضور والانصراف", "تطبيق", 0, "REP-004", 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeType", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "أتمتة الموارد", "تقني", 1, 3, 0, "REP-005", 2 });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeType", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, null, "تطوير سياسات العمل", "تنظيمي", 2, 7, 0, "REP-006", 2 });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeType", "ReadinessScore", "ReportNumber" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تفعيل منصة اعتماد", "إجرائي", 0, "REP-007" });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ChangeName", "ChangeType", "CurrentStage", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, null, "إعادة هيكلة الإدارات", "تنظيمي", "تطبيق", 0, "REP-008", 3 });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeType", "CurrentStage", "ReadinessScore", "ReportNumber" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تحديث أسطول النقل", "خدمي", "تهيئة", 0, "REP-009" });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "CurrentStage", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تطوير البنية التحتية", "تثبيت", 0, "REP-010", 3 });

            migrationBuilder.InsertData(
                table: "MonthlyReports",
                columns: new[] { "Id", "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionBarriers", "AdoptionScore", "AffectedGroups", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CommitteeRecommendation", "CreatedDate", "CurrentStage", "EvidenceLinks", "ExecutedActivities", "ImpactScope", "ImprovementOpportunities", "InitialRecommendation", "Month", "MostInNeedGroup", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Risks", "Status", "SubmittedDate", "SuccessStories", "WhatWillChange", "WhatWillNotChange", "WhyImportant", "Year" },
                values: new object[,]
                {
                    { 11, 0, 0, 0, 0, 0, 0, null, 0, null, null, null, "تفعيل الدوام المرن", null, "ثقافي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, null, null, null, "أغسطس", null, null, 2, 7, 0, "REP-011", null, null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 12, 0, 0, 0, 0, 0, 0, null, 0, null, null, null, "أتمتة المستودعات", null, "تقني", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, null, null, null, "أغسطس", null, null, 3, 9, 0, "REP-012", null, null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 13, 0, 0, 0, 0, 0, 0, null, 0, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "منصة التدريب", null, "تقني", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, null, null, null, null, "يوليو", null, null, 1, 3, 0, "REP-013", null, null, 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 14, 0, 0, 0, 0, 0, 0, null, 0, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "التأمين الطبي الجديد", null, "خدمي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, null, null, null, null, "يوليو", null, null, 2, 7, 0, "REP-014", null, null, 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 }
                });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "شهر", "رفع كفاءة الاستخدام", "نقص في التدريب التقني", 10, "اجتياز 80% من الموظفين للاختبار", "تخصيص ميزانية لتدريب 50 موظفاً." });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "أسبوعين", "استقرار بيئة العمل", "مقاومة التغيير لبيئة العمل", 11, "بانتظار قرار الدعم", "انخفاض الشكاوى", "إطلاق حملة توعوية بفوائد النظام." });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActionOwner", "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "إدارة التقنية", "فوري", "استمرار العمليات", "ضعف البنية التحتية", 12, "جاهزة لاعتماد رئيس اللجنة", "اختفاء الانقطاعات", "ترقية خوادم المستودعات" });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActionOwner", "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "الموارد البشرية", "مستمر", "إيقاف الورق تماماً", "استخدام النظام", 13, "R-07-001", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "100% نسبة تبني", "إلزام الإدارات بالمنصة الجديدة" });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActionOwner", "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "الاتصال", "أسبوع", "تقليل استفسارات الموظفين", "فهم البوليصة", 14, "R-07-002", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "توزيع الدليل لجميع الفروع", "نشر دليل مبسط للموظفين" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "FullName",
                value: "عضو اللجنة");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "اللجنة المركزية لإدارة وقياس أثر التغيير");

            migrationBuilder.InsertData(
                table: "EntityInputs",
                columns: new[] { "Id", "Content", "CreatedDate", "OrganizationEntityId", "PreparerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "تأخر الاستجابة من الدعم الفني يعيق التقدم.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 1 },
                    { 2, "انخفاض معدل التسرب الوظيفي بنسبة 15% بعد النظام الجديد.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 4, 2 },
                    { 3, "نقترح إضافة ميزة التوقيع الإلكتروني المتعدد.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 2, 3 },
                    { 4, "مقاومة تغيير عالية من الموظفين القدامى.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 7 },
                    { 5, "نحتاج ميزانية إضافية لتدريب 50 موظفاً.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 5, 5 },
                    { 6, "هل سيتم ربط النظام الجديد بمنصة اعتماد؟", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 1, 4 },
                    { 7, "واجهة المستخدم في الشاشات بطيئة التجاوب.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 6 },
                    { 8, "تخفيض وقت معالجة الطلبات من 3 أيام إلى ساعتين.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 4, 2 },
                    { 9, "انقطاع النظام المتكرر وقت الذروة.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 2, 1 },
                    { 10, "أتمتة إرسال التقارير عبر البريد الإلكتروني.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 3 }
                });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "CurrentStage", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 85, 30, 90, 60, 70, 40, 55, "نظام الاتصالات الإدارية", "تطبيق", 62, "CTP-2026-08-A11", 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeType", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 40, 40, 70, 80, 50, 20, 45, null, "هيكلة الموارد البشرية", "تنظيمي", 2, 7, 52, "CTP-2026-08-B22", 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ChangeName", "CurrentStage", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 100, 80, 95, 90, 85, 90, 88, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "منصة سلاسل الإمداد", "تثبيت", 3, 9, 88, "CTP-2026-08-C33", 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActivityCompletionRate", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdoptionScore", "ChangeName", "CurrentStage", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate" },
                values: new object[] { 10, 40, 30, 10, 5, "بوابة الموردين", "تعريف", 16, "CTP-2026-08-D44", 1, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeType", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 60, 50, 80, 85, 60, 40, 58, "سياسة العمل المرن", "ثقافي", 2, 7, 63, "CTP-2026-08-E55", 3 });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeType", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 50, 20, 60, 50, 40, 10, 25, "يرجى توضيح الميزانية", "تحديث أسطول النقل", "خدمي", 3, 9, 36, "CTP-2026-08-F66", 4 });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeType", "ReadinessScore", "ReportNumber" },
                values: new object[] { 75, 60, 85, 70, 80, 50, 65, "نظام الأرشفة المركزية", "تقني", 69, "CTP-2026-08-G77" });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ChangeName", "ChangeType", "CurrentStage", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 95, 85, 90, 95, 90, 80, 85, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "برنامج الولاء الوظيفي", "ثقافي", "تثبيت", 88, "CTP-2026-08-H88", 5 });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeType", "CurrentStage", "ReadinessScore", "ReportNumber" },
                values: new object[] { 80, 55, 80, 75, 70, 45, 55, "مركز التحكم والسيطرة", "تنظيمي", "تطبيق", 65, "CTP-2026-08-I99" });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "CurrentStage", "ReadinessScore", "ReportNumber", "Status" },
                values: new object[] { 20, 10, 50, 40, 20, 5, 15, "الذكاء الاصطناعي التوليدي", "تعريف", 25, "CTP-2026-08-J10", 2 });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "أسبوعين", "تقليل الأخطاء", "القدرة (Ability)", 1, "زيادة الاستخدام 30%", "تكثيف ورش العمل التطبيقية للموظفين." });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "شهر", "رفع معنويات الموظفين", "التعزيز (Reinforcement)", 5, "جاهزة لاعتماد رئيس اللجنة", "تكريم 50 موظف", "إطلاق حملة مكافآت للمتبنين الأوائل." });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActionOwner", "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "التحول الرقمي", "3 أسابيع", "استقلالية الموظف", "المعرفة (Knowledge)", 7, "بانتظار قرار الدعم", "انخفاض تذاكر الدعم بـ 40%", "توفير أدلة استخدام مرئية ومقاطع فيديو قصيرة." });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActionOwner", "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "مكتب القائد", "أسبوع", "إيصال رؤية التغيير", "الوعي (Awareness)", 9, "R-08-004", new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "حضور 80% من المستهدفين", "عقد لقاء مفتوح مع قيادات القطاع." });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActionOwner", "Duration", "ExpectedImpact", "GapType", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "SuccessIndicator", "SuggestedAction" },
                values: new object[] { "الموارد البشرية", "فوري", "ضمان الاستدامة", "التعزيز (Reinforcement)", 3, "R-08-005", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "إصدار تعميم رسمي", "ربط الاستخدام بتقييم الأداء السنوي." });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "ActionOwner", "ChairReviewNotes", "CreatedDate", "Duration", "ExpectedImpact", "GapType", "IncludeInExecutiveSummary", "IncludeInImpactDashboard", "IncludeInInstitutionalReport", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[,]
                {
                    { 6, "المشتريات", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهران", "تسريع الإنجاز الميداني", "القدرة (Ability)", false, false, false, 8, "R-08-006", new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة مباشرة (إدراج واعتماد رئيس اللجنة)", "تسليم 200 جهاز", "توفير أجهزة لوحية لموظفي العمليات الميدانية." },
                    { 7, "لجنة التغيير", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تقليل المقاومة", "الرغبة (Desire)", false, false, false, 1, "R-08-007", null, "جاهزة لاعتماد رئيس اللجنة", "تعيين 10 سفراء جدد", "تشكيل شبكة سفراء تغيير داخلية في الإدارات الرافضة." },
                    { 8, "إدارة الجودة", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهر", "تسريع الإجراءات", "القدرة (Ability)", false, false, false, 5, "R-08-008", null, "مستبدلة بتوصية سيادية", "اعتماد النماذج المبسطة", "تبسيط نماذج العمل وإلغاء الموافقات المعقدة." },
                    { 9, "تقنية المعلومات", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهران", "نقل المعرفة", "المعرفة (Knowledge)", false, false, false, 7, "R-08-009", null, "بانتظار قرار الدعم", "إضافة 50 مقال جديد", "تحديث بوابة المعرفة المؤسسية (Wiki)." },
                    { 10, "الاتصال المؤسسي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مستمر", "إبراز المنجزات", "التعزيز (Reinforcement)", false, false, false, 9, "R-08-010", null, "جاهزة لاعتماد رئيس اللجنة", "نشر 4 قصص شهرياً", "نشر قصص النجاح الأسبوعية عبر إيميل القطاع." }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "FullName",
                value: "عضو اللجنة (أحمد)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "Email", "FailedLoginAttempts", "FullName", "IsActive", "JobTitle", "LastLogin", "LockedUntil", "MustChangePassword", "OrganizationEntityId", "PasswordExpiresDate", "PasswordHash", "RefreshToken", "RefreshTokenExpiresDate", "Username" },
                values: new object[] { 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "عضو اللجنة (سعد)", true, null, null, null, false, 2, null, "123456", null, null, "member2" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[] { 5, 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 10 });
        }
    }
}
