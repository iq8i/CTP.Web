using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMassiveStagingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "EntityInputs",
                columns: new[] { "Id", "Content", "CreatedDate", "OrganizationEntityId", "PreparerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "هذا النص يمثل محتوى المدخل رقم 1، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 5, 6 },
                    { 2, "هذا النص يمثل محتوى المدخل رقم 2، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 1, 7 },
                    { 3, "هذا النص يمثل محتوى المدخل رقم 3، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2, 4 },
                    { 4, "هذا النص يمثل محتوى المدخل رقم 4، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 5, 4 },
                    { 5, "هذا النص يمثل محتوى المدخل رقم 5، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 5, 2 },
                    { 6, "هذا النص يمثل محتوى المدخل رقم 6، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 3 },
                    { 7, "هذا النص يمثل محتوى المدخل رقم 7، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 5, 5 },
                    { 8, "هذا النص يمثل محتوى المدخل رقم 8، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 3, 1 },
                    { 9, "هذا النص يمثل محتوى المدخل رقم 9، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2, 6 },
                    { 10, "هذا النص يمثل محتوى المدخل رقم 10، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 2, 3 },
                    { 11, "هذا النص يمثل محتوى المدخل رقم 11، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 5, 2 },
                    { 12, "هذا النص يمثل محتوى المدخل رقم 12، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 5, 1 },
                    { 13, "هذا النص يمثل محتوى المدخل رقم 13، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 4, 1 },
                    { 14, "هذا النص يمثل محتوى المدخل رقم 14، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 2, 4 },
                    { 15, "هذا النص يمثل محتوى المدخل رقم 15، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 1 },
                    { 16, "هذا النص يمثل محتوى المدخل رقم 16، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 5, 6 },
                    { 17, "هذا النص يمثل محتوى المدخل رقم 17، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 5, 4 },
                    { 18, "هذا النص يمثل محتوى المدخل رقم 18، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 5, 7 },
                    { 19, "هذا النص يمثل محتوى المدخل رقم 19، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 4, 6 },
                    { 20, "هذا النص يمثل محتوى المدخل رقم 20، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 1, 3 }
                });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 42, 99, 32, 42, 93, 93, 96, "نظام الأداء (1)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 55, "REP-2026-001", "توفير ميزانية إضافية للتدريب.", "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 11, 30, 35, 52, 60, 44, 37, null, "منصة اعتماد (2)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 49, "REP-2026-002", "توفير ميزانية إضافية للتدريب.", 1, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 20, 69, 51, 84, 40, 85, 77, "منصة اعتماد (3)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 58, "REP-2026-003", "توفير ميزانية إضافية للتدريب.", "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 35, 41, 82, 26, 65, 27, 34, "بوابة الموظفين (4)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 57, "REP-2026-004", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 31, 35, 26, 97, 70, 25, 30, "الأرشفة الرقمية (5)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 64, "REP-2026-005", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 29, 78, 40, 90, 28, 44, 61, "نظام الأداء (6)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 52, "REP-2026-006", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 48, 64, 35, 70, 54, 23, 43, "خدمات المستفيدين (7)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 53, "REP-2026-007", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 17, 99, 88, 77, 43, 69, 84, "منصة اعتماد (8)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 69, "REP-2026-008", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 46, 32, 93, 85, 74, 49, 40, "نظام الاتصالات (9)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 84, "REP-2026-009", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "CreatedDate", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 37, 81, 43, 82, 99, 61, 71, "بوابة الموظفين (10)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 74, "REP-2026-010", "توفير ميزانية إضافية للتدريب.", 1, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 92, 55, 71, 22, 51, 44, 49, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "الأرشفة الرقمية (11)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 48, "REP-2026-011", 4, new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 29, 91, 76, 68, 90, 23, 57, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "منصة اعتماد (12)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "تأخر تجاوب بعض الإدارات التقنية.", 78, "REP-2026-012", 4, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Month", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 53, 53, 60, 99, 88, 36, 44, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "تطوير الهيكل (13)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "أغسطس", "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 82, "REP-2026-013", 4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Month", "Obstacles", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 40, 54, 81, 93, 74, 58, 56, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "الدوام المرن (14)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", "أغسطس", "تأخر تجاوب بعض الإدارات التقنية.", 82, "REP-2026-014", 4, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة." });

            migrationBuilder.InsertData(
                table: "MonthlyReports",
                columns: new[] { "Id", "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionBarriers", "AdoptionScore", "AffectedGroups", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CommitteeRecommendation", "CreatedDate", "CurrentStage", "EvidenceLinks", "ExecutedActivities", "ImpactScope", "ImprovementOpportunities", "InitialRecommendation", "Month", "MostInNeedGroup", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Risks", "Status", "SubmittedDate", "SuccessStories", "WhatWillChange", "WhatWillNotChange", "WhyImportant", "Year" },
                values: new object[,]
                {
                    { 15, 96, 36, 70, 25, 46, 76, null, 56, null, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "خدمات المستفيدين (15)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 47, "REP-2026-015", null, null, 4, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 16, 31, 49, 82, 43, 47, 71, null, 60, null, null, null, "ترقية الخوادم (16)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 57, "REP-2026-016", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 17, 72, 35, 30, 75, 39, 79, null, 57, null, null, null, "خدمات المستفيدين (17)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 48, "REP-2026-017", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 18, 42, 90, 39, 25, 63, 67, null, 78, null, null, null, "منصة اعتماد (18)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 42, "REP-2026-018", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 19, 27, 30, 20, 27, 81, 49, null, 39, null, null, null, "الأرشفة الرقمية (19)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 42, "REP-2026-019", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 20, 27, 50, 45, 76, 74, 30, null, 40, null, null, null, "نظام الأداء (20)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 65, "REP-2026-020", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 21, 14, 76, 93, 27, 70, 27, null, 51, null, null, null, "منصة اعتماد (21)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", null, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 63, "REP-2026-021", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 22, 57, 69, 49, 76, 83, 97, null, 83, null, null, null, "منصة اعتماد (22)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 69, "REP-2026-022", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 23, 27, 37, 23, 81, 73, 79, null, 58, null, null, null, "نظام الأداء (23)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 59, "REP-2026-023", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 24, 13, 69, 55, 21, 43, 24, null, 46, null, null, null, "ترقية الخوادم (24)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 39, "REP-2026-024", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 25, 76, 28, 97, 20, 29, 20, null, 24, null, null, null, "أتمتة المشتريات (25)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 48, "REP-2026-025", "توفير ميزانية إضافية للتدريب.", null, 2, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 26, 56, 58, 71, 94, 21, 49, null, 53, null, null, null, "نظام الأداء (26)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 62, "REP-2026-026", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 27, 33, 74, 27, 66, 99, 64, null, 69, null, null, null, "بوابة الموظفين (27)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 64, "REP-2026-027", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 28, 96, 92, 55, 66, 84, 70, null, 81, null, null, null, "الأرشفة الرقمية (28)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", null, new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 68, "REP-2026-028", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 29, 24, 30, 43, 58, 92, 48, null, 39, null, null, null, "ترقية الخوادم (29)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 64, "REP-2026-029", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 30, 35, 38, 26, 91, 86, 48, null, 43, null, null, null, "أتمتة المشتريات (30)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 67, "REP-2026-030", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 31, 88, 73, 65, 81, 63, 81, null, 77, null, null, null, "أتمتة المشتريات (31)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 69, "REP-2026-031", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 32, 44, 92, 51, 39, 72, 46, null, 69, null, null, null, "ترقية الخوادم (32)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 54, "REP-2026-032", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 33, 20, 77, 99, 61, 47, 50, null, 63, null, null, null, "الدوام المرن (33)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 69, "REP-2026-033", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 34, 77, 48, 44, 40, 74, 65, null, 56, null, null, null, "خدمات المستفيدين (34)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 52, "REP-2026-034", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 35, 82, 90, 98, 74, 24, 48, null, 69, null, null, null, "تطوير الهيكل (35)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 65, "REP-2026-035", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 36, 15, 21, 29, 89, 80, 55, null, 38, null, null, null, "نظام الأداء (36)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 66, "REP-2026-036", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 37, 30, 75, 29, 64, 77, 64, null, 69, null, null, null, "تطوير الهيكل (37)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 56, "REP-2026-037", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 38, 49, 74, 62, 64, 72, 92, null, 83, null, null, null, "نظام الأداء (38)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 66, "REP-2026-038", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 39, 97, 31, 29, 84, 80, 29, null, 30, null, null, null, "الدوام المرن (39)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 64, "REP-2026-039", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 40, 15, 45, 23, 61, 70, 66, null, 55, null, null, null, "أتمتة المشتريات (40)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 51, "REP-2026-040", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 41, 11, 44, 77, 96, 48, 53, null, 48, null, null, null, "أتمتة المشتريات (41)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", null, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 73, "REP-2026-041", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 42, 87, 29, 89, 27, 30, 83, null, 56, null, null, null, "نظام الأداء (42)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 48, "REP-2026-042", "توفير ميزانية إضافية للتدريب.", null, 3, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 43, 88, 31, 80, 45, 93, 90, null, 60, null, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "أتمتة المشتريات (43)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 72, "REP-2026-043", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 44, 51, 57, 30, 59, 98, 27, null, 42, null, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "نظام الاتصالات (44)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 62, "REP-2026-044", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 45, 18, 95, 27, 45, 54, 28, null, 61, null, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "بوابة الموظفين (45)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 42, "REP-2026-045", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 46, 38, 55, 97, 55, 60, 99, null, 77, null, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ترقية الخوادم (46)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 70, "REP-2026-046", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 47, 33, 90, 50, 69, 55, 89, null, 89, null, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "خدمات المستفيدين (47)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 58, "REP-2026-047", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 48, 62, 66, 65, 87, 38, 51, null, 58, null, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "نظام الاتصالات (48)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 63, "REP-2026-048", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 49, 72, 53, 85, 46, 62, 99, null, 76, null, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "خدمات المستفيدين (49)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 64, "REP-2026-049", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 50, 21, 75, 79, 62, 47, 34, null, 54, null, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "نظام الاتصالات (50)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 62, "REP-2026-050", "توفير ميزانية إضافية للتدريب.", null, 5, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "RoleName",
                value: "قادة الوحدات");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5,
                column: "RoleName",
                value: "أعضاء اللجنة");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6,
                column: "RoleName",
                value: "رئيس اللجنة");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7,
                column: "RoleName",
                value: "رئيس فريق عمل القائد");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 8,
                column: "RoleName",
                value: "نائب القائد");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 9,
                column: "RoleName",
                value: "القائد");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "FullName",
                value: "سفير التغيير 1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "FullName",
                value: "قائد وحدة 1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "FullName",
                value: "سفير التغيير 2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "FullName",
                value: "قائد وحدة 2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "FullName",
                value: "سفير التغيير 3");

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "ActionOwner", "ChairReviewNotes", "CreatedDate", "Duration", "ExpectedImpact", "GapType", "IncludeInExecutiveSummary", "IncludeInImpactDashboard", "IncludeInInstitutionalReport", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[,]
                {
                    { 36, "إدارة التواصل", null, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 36, "REC-036", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 37, "إدارة التواصل", null, new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 37, "REC-037", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 38, "إدارة التواصل", null, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 38, "REC-038", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 39, "إدارة التواصل", null, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 39, "REC-039", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 40, "إدارة التواصل", null, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 40, "REC-040", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 41, "إدارة التواصل", null, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 41, "REC-041", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 42, "إدارة التواصل", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 42, "REC-042", null, "جاهزة لاعتماد رئيس اللجنة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 43, "إدارة التواصل", null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 43, "REC-043", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 44, "إدارة التواصل", null, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 44, "REC-044", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 45, "إدارة التواصل", null, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 45, "REC-045", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 46, "إدارة التواصل", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 46, "REC-046", new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 47, "إدارة التواصل", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 47, "REC-047", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 48, "إدارة التواصل", null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 48, "REC-048", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 49, "إدارة التواصل", null, new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 49, "REC-049", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 50, "إدارة التواصل", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 50, "REC-050", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EntityInputs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "مسودة مشروع الأتمتة", null, "تقني", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, null, 0, "REP-001", null, null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "يرجى تفصيل الميزانية المطلوبة", "تهيئة البيئة السحابية", null, "تقني", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, null, 1, 3, 0, "REP-002", null, 4, null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "نظام التذاكر", null, "إجرائي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, "REP-003", null, null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تطبيق الحضور والانصراف", null, "تقني", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, "REP-004", null, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "أتمتة الموارد", null, "تقني", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, 1, 3, 0, "REP-005", null, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تطوير سياسات العمل", null, "تنظيمي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, null, 2, 7, 0, "REP-006", null, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تفعيل منصة اعتماد", null, "إجرائي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, 1, 3, 0, "REP-007", null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "إعادة هيكلة الإدارات", null, "تنظيمي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, 2, 7, 0, "REP-008", null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تحديث أسطول النقل", null, "خدمي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, null, 0, "REP-009", null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ChangeName", "ChangeSummary", "CreatedDate", "ExecutedActivities", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, "تطوير البنية التحتية", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, 3, 0, "REP-010", null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, null, "تفعيل الدوام المرن", null, "ثقافي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, 0, "REP-011", 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "ExecutedActivities", "Obstacles", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, null, "أتمتة المستودعات", null, "تقني", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, "REP-012", 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Month", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "منصة التدريب", null, "تقني", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "يوليو", null, 1, 3, 0, "REP-013", 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.UpdateData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionScore", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CreatedDate", "CurrentStage", "ExecutedActivities", "Month", "Obstacles", "ReadinessScore", "ReportNumber", "Status", "SubmittedDate", "WhatWillChange", "WhatWillNotChange", "WhyImportant" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "التأمين الطبي الجديد", null, "خدمي", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "يوليو", null, 0, "REP-014", 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "ActionOwner", "ChairReviewNotes", "CreatedDate", "Duration", "ExpectedImpact", "GapType", "IncludeInExecutiveSummary", "IncludeInImpactDashboard", "IncludeInInstitutionalReport", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[,]
                {
                    { 1, "إدارة التدريب", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهر", "رفع كفاءة الاستخدام", "نقص في التدريب التقني", false, false, false, 10, "R-08-001", null, "جاهزة لاعتماد رئيس اللجنة", "اجتياز 80% من الموظفين للاختبار", "تخصيص ميزانية لتدريب 50 موظفاً." },
                    { 2, "الاتصال المؤسسي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أسبوعين", "استقرار بيئة العمل", "مقاومة التغيير لبيئة العمل", false, false, false, 11, "R-08-002", null, "بانتظار قرار الدعم", "انخفاض الشكاوى", "إطلاق حملة توعوية بفوائد النظام." },
                    { 3, "إدارة التقنية", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "فوري", "استمرار العمليات", "ضعف البنية التحتية", false, false, false, 12, "R-08-003", null, "جاهزة لاعتماد رئيس اللجنة", "اختفاء الانقطاعات", "ترقية خوادم المستودعات" },
                    { 4, "الموارد البشرية", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مستمر", "إيقاف الورق تماماً", "استخدام النظام", false, false, false, 13, "R-07-001", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "100% نسبة تبني", "إلزام الإدارات بالمنصة الجديدة" },
                    { 5, "الاتصال", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أسبوع", "تقليل استفسارات الموظفين", "فهم البوليصة", false, false, false, 14, "R-07-002", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "توزيع الدليل لجميع الفروع", "نشر دليل مبسط للموظفين" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "RoleName",
                value: "أصحاب السعادة قادة الوحدات والمدراء");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5,
                column: "RoleName",
                value: "أعضاء لجنة شركاء التغيير");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6,
                column: "RoleName",
                value: "رئيس لجنة شركاء التغيير");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7,
                column: "RoleName",
                value: "سعادة رئيس فريق عمل القائد");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 8,
                column: "RoleName",
                value: "سعادة نائب القائد");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 9,
                column: "RoleName",
                value: "معالي القائد");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "FullName",
                value: "سفير التغيير (التقنية)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "FullName",
                value: "قائد وحدة التقنية");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "FullName",
                value: "سفير التغيير (الموارد البشرية)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "FullName",
                value: "مدير الموارد البشرية");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "FullName",
                value: "سفير التغيير (العمليات)");
        }
    }
}
