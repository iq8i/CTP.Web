using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedExpandedStagingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 9, 9 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.InsertData(
                table: "Committees",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Name" },
                values: new object[] { 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "اللجنة المركزية لإدارة وقياس أثر التغيير", true, "لجنة شركاء التغيير" });

            migrationBuilder.InsertData(
                table: "EntityInputs",
                columns: new[] { "Id", "Content", "CreatedDate", "OrganizationEntityId", "PreparerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "تأخر الاستجابة من الدعم الفني يعيق التقدم.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 1 },
                    { 4, "مقاومة تغيير عالية من الموظفين القدامى.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 7 },
                    { 7, "واجهة المستخدم في الشاشات بطيئة التجاوب.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 6 },
                    { 10, "أتمتة إرسال التقارير عبر البريد الإلكتروني.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "MonthlyReports",
                columns: new[] { "Id", "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionBarriers", "AdoptionScore", "AffectedGroups", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CommitteeRecommendation", "CreatedDate", "CurrentStage", "EvidenceLinks", "ExecutedActivities", "ImpactScope", "ImprovementOpportunities", "InitialRecommendation", "Month", "MostInNeedGroup", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Risks", "Status", "SubmittedDate", "SuccessStories", "WhatWillChange", "WhatWillNotChange", "WhyImportant", "Year" },
                values: new object[,]
                {
                    { 1, 85, 30, 90, 60, 70, 40, null, 55, null, null, null, "نظام الاتصالات الإدارية", null, "تقني", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, null, null, null, "أغسطس", null, null, 1, 3, 62, "CTP-2026-08-A11", null, null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 4, 10, 0, 40, 30, 10, 0, null, 5, null, null, null, "بوابة الموردين", null, "تقني", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, null, null, null, null, "أغسطس", null, null, 1, 3, 16, "CTP-2026-08-D44", null, null, 1, null, null, null, null, null, 2026 },
                    { 7, 75, 60, 85, 70, 80, 50, null, 65, null, null, null, "نظام الأرشفة المركزية", null, "تقني", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, null, null, null, "أغسطس", null, null, 1, 3, 69, "CTP-2026-08-G77", null, null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 10, 20, 10, 50, 40, 20, 5, null, 15, null, null, null, "الذكاء الاصطناعي التوليدي", null, "تقني", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, null, null, null, null, "أغسطس", null, null, 1, 3, 25, "CTP-2026-08-J10", null, null, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 }
                });

            migrationBuilder.UpdateData(
                table: "OrganizationEntities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "OrganizationEntities",
                columns: new[] { "Id", "CreatedDate", "EntityCode", "EntityName", "IsActive" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UNIT-02", "إدارة الموارد البشرية", true },
                    { 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UNIT-03", "قطاع العمليات", true }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "AssignedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "AssignedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 },
                column: "AssignedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 4 },
                column: "AssignedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 5 },
                column: "AssignedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 6, 6 },
                column: "AssignedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[,]
                {
                    { 3, 7, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 7 },
                    { 4, 8, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 8 },
                    { 3, 9, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 9 },
                    { 5, 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "FullName", "Username" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سفير التغيير (التقنية)", "ambassador1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "قائد وحدة التقنية" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "عضو اللجنة (أحمد)" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سفير التغيير (الموارد البشرية)", 2, "ambassador2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مدير الموارد البشرية", 2, "unitleader2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سفير التغيير (العمليات)", 3, "ambassador3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "عضو اللجنة (سعد)", 2, "member2" });

            migrationBuilder.InsertData(
                table: "EntityInputs",
                columns: new[] { "Id", "Content", "CreatedDate", "OrganizationEntityId", "PreparerId", "Status", "Type" },
                values: new object[,]
                {
                    { 2, "انخفاض معدل التسرب الوظيفي بنسبة 15% بعد النظام الجديد.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 4, 2 },
                    { 3, "نقترح إضافة ميزة التوقيع الإلكتروني المتعدد.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 2, 3 },
                    { 5, "نحتاج ميزانية إضافية لتدريب 50 موظفاً.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 5, 5 },
                    { 6, "هل سيتم ربط النظام الجديد بمنصة اعتماد؟", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 1, 4 },
                    { 8, "تخفيض وقت معالجة الطلبات من 3 أيام إلى ساعتين.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 4, 2 },
                    { 9, "انقطاع النظام المتكرر وقت الذروة.", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "MonthlyReports",
                columns: new[] { "Id", "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionBarriers", "AdoptionScore", "AffectedGroups", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CommitteeRecommendation", "CreatedDate", "CurrentStage", "EvidenceLinks", "ExecutedActivities", "ImpactScope", "ImprovementOpportunities", "InitialRecommendation", "Month", "MostInNeedGroup", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Risks", "Status", "SubmittedDate", "SuccessStories", "WhatWillChange", "WhatWillNotChange", "WhyImportant", "Year" },
                values: new object[,]
                {
                    { 2, 40, 40, 70, 80, 50, 20, null, 45, null, null, null, "هيكلة الموارد البشرية", null, "تنظيمي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, null, null, null, null, "أغسطس", null, null, 2, 7, 52, "CTP-2026-08-B22", null, null, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 3, 100, 80, 95, 90, 85, 90, null, 88, null, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "منصة سلاسل الإمداد", null, "إجرائي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, null, null, null, null, "أغسطس", null, null, 3, 9, 88, "CTP-2026-08-C33", null, null, 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 5, 60, 50, 80, 85, 60, 40, null, 58, null, null, null, "سياسة العمل المرن", null, "ثقافي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, null, null, null, "أغسطس", null, null, 2, 7, 63, "CTP-2026-08-E55", null, null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 6, 50, 20, 60, 50, 40, 10, null, 25, null, null, "يرجى توضيح الميزانية", "تحديث أسطول النقل", null, "خدمي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, null, null, null, null, "أغسطس", null, null, 3, 9, 36, "CTP-2026-08-F66", null, null, 4, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 8, 95, 85, 90, 95, 90, 80, null, 85, null, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "برنامج الولاء الوظيفي", null, "ثقافي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, null, null, null, null, "أغسطس", null, null, 2, 7, 88, "CTP-2026-08-H88", null, null, 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 },
                    { 9, 80, 55, 80, 75, 70, 45, null, 55, null, null, null, "مركز التحكم والسيطرة", null, "تنظيمي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, null, null, null, null, "أغسطس", null, null, 3, 9, 65, "CTP-2026-08-I99", null, null, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, 2026 }
                });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "ActionOwner", "ChairReviewNotes", "CreatedDate", "Duration", "ExpectedImpact", "GapType", "IncludeInExecutiveSummary", "IncludeInImpactDashboard", "IncludeInInstitutionalReport", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[,]
                {
                    { 1, "إدارة التدريب", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أسبوعين", "تقليل الأخطاء", "القدرة (Ability)", false, false, false, 1, "R-08-001", null, "جاهزة لاعتماد رئيس اللجنة", "زيادة الاستخدام 30%", "تكثيف ورش العمل التطبيقية للموظفين." },
                    { 3, "التحول الرقمي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "استقلالية الموظف", "المعرفة (Knowledge)", false, false, false, 7, "R-08-003", null, "بانتظار قرار الدعم", "انخفاض تذاكر الدعم بـ 40%", "توفير أدلة استخدام مرئية ومقاطع فيديو قصيرة." },
                    { 7, "لجنة التغيير", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تقليل المقاومة", "الرغبة (Desire)", false, false, false, 1, "R-08-007", null, "جاهزة لاعتماد رئيس اللجنة", "تعيين 10 سفراء جدد", "تشكيل شبكة سفراء تغيير داخلية في الإدارات الرافضة." },
                    { 9, "تقنية المعلومات", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهران", "نقل المعرفة", "المعرفة (Knowledge)", false, false, false, 7, "R-08-009", null, "بانتظار قرار الدعم", "إضافة 50 مقال جديد", "تحديث بوابة المعرفة المؤسسية (Wiki)." },
                    { 2, "الاتصال المؤسسي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهر", "رفع معنويات الموظفين", "التعزيز (Reinforcement)", false, false, false, 5, "R-08-002", null, "جاهزة لاعتماد رئيس اللجنة", "تكريم 50 موظف", "إطلاق حملة مكافآت للمتبنين الأوائل." },
                    { 4, "مكتب القائد", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أسبوع", "إيصال رؤية التغيير", "الوعي (Awareness)", false, false, false, 9, "R-08-004", new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "حضور 80% من المستهدفين", "عقد لقاء مفتوح مع قيادات القطاع." },
                    { 5, "الموارد البشرية", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "فوري", "ضمان الاستدامة", "التعزيز (Reinforcement)", false, false, false, 3, "R-08-005", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة", "إصدار تعميم رسمي", "ربط الاستخدام بتقييم الأداء السنوي." },
                    { 6, "المشتريات", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهران", "تسريع الإنجاز الميداني", "القدرة (Ability)", false, false, false, 8, "R-08-006", new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "معتمدة مباشرة (إدراج واعتماد رئيس اللجنة)", "تسليم 200 جهاز", "توفير أجهزة لوحية لموظفي العمليات الميدانية." },
                    { 8, "إدارة الجودة", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهر", "تسريع الإجراءات", "القدرة (Ability)", false, false, false, 5, "R-08-008", null, "مستبدلة بتوصية سيادية", "اعتماد النماذج المبسطة", "تبسيط نماذج العمل وإلغاء الموافقات المعقدة." },
                    { 10, "الاتصال المؤسسي", null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مستمر", "إبراز المنجزات", "التعزيز (Reinforcement)", false, false, false, 9, "R-08-010", null, "جاهزة لاعتماد رئيس اللجنة", "نشر 4 قصص شهرياً", "نشر قصص النجاح الأسبوعية عبر إيميل القطاع." }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs",
                column: "PreparerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs");

            migrationBuilder.DeleteData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 1);

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
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 10);

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
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MonthlyReports",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrganizationEntities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrganizationEntities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "OrganizationEntities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "AssignedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "AssignedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 },
                column: "AssignedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 4 },
                column: "AssignedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 5 },
                column: "AssignedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 6, 6 },
                column: "AssignedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[,]
                {
                    { 7, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 7 },
                    { 8, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 8 },
                    { 9, 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 9 },
                    { 10, 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "FullName", "Username" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سفير التغيير", "ambassador" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "قائد الوحدة" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "عضو اللجنة" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "رئيس فريق عمل القائد", 1, "chief" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نائب القائد", 1, "deputy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "معالي القائد", 1, "leader" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                columns: new[] { "CreatedDate", "FullName", "OrganizationEntityId", "Username" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "موظف الاتصال", 1, "comms" });

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs",
                column: "PreparerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
