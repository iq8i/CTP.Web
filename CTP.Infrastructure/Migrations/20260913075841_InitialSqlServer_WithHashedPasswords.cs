using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlServer_WithHashedPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrganizationEntityId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    PasswordExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    LockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationEntityId = table.Column<int>(type: "int", nullable: false),
                    PreparerId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityInputs_Users_PreparerId",
                        column: x => x.PreparerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrganizationEntityId = table.Column<int>(type: "int", nullable: false),
                    PreparerId = table.Column<int>(type: "int", nullable: false),
                    ChangeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CurrentStage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AffectedGroups = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImpactScope = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MostInNeedGroup = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ActivityCompletionRate = table.Column<int>(type: "int", nullable: false),
                    ChangeSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhyImportant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatWillChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatWillNotChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutedActivities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdkarAwareness = table.Column<int>(type: "int", nullable: false),
                    AdkarDesire = table.Column<int>(type: "int", nullable: false),
                    AdkarKnowledge = table.Column<int>(type: "int", nullable: false),
                    AdkarAbility = table.Column<int>(type: "int", nullable: false),
                    AdkarReinforcement = table.Column<int>(type: "int", nullable: false),
                    ReadinessScore = table.Column<int>(type: "int", nullable: false),
                    AdoptionScore = table.Column<int>(type: "int", nullable: false),
                    Obstacles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdoptionBarriers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Risks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImprovementOpportunities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessStories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommitteeRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApproverNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvidenceLinks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyReports_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonthlyReports_Users_PreparerId",
                        column: x => x.PreparerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TextColorClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MonthlyReportId = table.Column<int>(type: "int", nullable: false),
                    GapType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SuggestedAction = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ActionOwner = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SuccessIndicator = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpectedImpact = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChairReviewNotes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IncludeInInstitutionalReport = table.Column<bool>(type: "bit", nullable: false),
                    IncludeInImpactDashboard = table.Column<bool>(type: "bit", nullable: false),
                    IncludeInExecutiveSummary = table.Column<bool>(type: "bit", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_MonthlyReports_MonthlyReportId",
                        column: x => x.MonthlyReportId,
                        principalTable: "MonthlyReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Committees",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Name" },
                values: new object[] { 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "لجنة شركاء التغيير" });

            migrationBuilder.InsertData(
                table: "OrganizationEntities",
                columns: new[] { "Id", "CreatedDate", "EntityCode", "EntityName", "IsActive" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UNIT-01", "وحدة التحول الرقمي", true },
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UNIT-02", "إدارة الموارد البشرية", true },
                    { 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UNIT-03", "قطاع العمليات", true }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "Description", "IsActive", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "STAFF", "المنسوبون" },
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "MANAGER", "القادة والمدراء المباشرون" },
                    { 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "AMBASSADOR", "سفراء التغيير" },
                    { 4, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "UNIT_LEADER", "قادة الوحدات" },
                    { 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "COMMITTEE_MEMBER", "أعضاء اللجنة" },
                    { 6, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "COMMITTEE_CHAIR", "رئيس اللجنة" },
                    { 7, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "CHIEF_OF_STAFF", "رئيس فريق عمل القائد" },
                    { 8, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "DEPUTY_LEADER", "نائب القائد" },
                    { 9, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "LEADER", "القائد" },
                    { 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "CORP_COMMS", "الاتصال المؤسسي" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "Email", "FailedLoginAttempts", "FullName", "IsActive", "JobTitle", "LastLogin", "LockedUntil", "MustChangePassword", "OrganizationEntityId", "PasswordExpiresDate", "PasswordHash", "RefreshToken", "RefreshTokenExpiresDate", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "مستخدم منسوب", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "staff" },
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "مدير مباشر", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "manager" },
                    { 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "سفير التغيير 1", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "ambassador1" },
                    { 4, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "قائد وحدة 1", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "unitleader" },
                    { 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "عضو اللجنة", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "member" },
                    { 6, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "رئيس اللجنة", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "chair" },
                    { 7, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "سفير التغيير 2", true, null, null, null, false, 2, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "ambassador2" },
                    { 8, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "قائد وحدة 2", true, null, null, null, false, 2, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "unitleader2" },
                    { 9, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "سفير التغيير 3", true, null, null, null, false, 3, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "ambassador3" },
                    { 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "رئيس فريق عمل القائد", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "chief" },
                    { 11, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "نائب القائد", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "deputy" },
                    { 12, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "معالي القائد", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "leader" },
                    { 13, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "الاتصال المؤسسي", true, null, null, null, false, 1, null, "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy", null, null, "comms" }
                });

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

            migrationBuilder.InsertData(
                table: "MonthlyReports",
                columns: new[] { "Id", "ActivityCompletionRate", "AdkarAbility", "AdkarAwareness", "AdkarDesire", "AdkarKnowledge", "AdkarReinforcement", "AdoptionBarriers", "AdoptionScore", "AffectedGroups", "ApprovedDate", "ApproverNotes", "ChangeName", "ChangeSummary", "ChangeType", "CommitteeRecommendation", "CreatedDate", "CurrentStage", "EvidenceLinks", "ExecutedActivities", "ImpactScope", "ImprovementOpportunities", "InitialRecommendation", "Month", "MostInNeedGroup", "Obstacles", "OrganizationEntityId", "PreparerId", "ReadinessScore", "ReportNumber", "RequiredSupport", "Risks", "Status", "SubmittedDate", "SuccessStories", "WhatWillChange", "WhatWillNotChange", "WhyImportant", "Year" },
                values: new object[,]
                {
                    { 1, 42, 99, 32, 42, 93, 93, null, 96, null, null, null, "نظام الأداء (1)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 55, "REP-2026-001", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 2, 11, 30, 35, 52, 60, 44, null, 37, null, null, null, "منصة اعتماد (2)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 49, "REP-2026-002", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 3, 20, 69, 51, 84, 40, 85, null, 77, null, null, null, "منصة اعتماد (3)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", null, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 58, "REP-2026-003", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 4, 35, 41, 82, 26, 65, 27, null, 34, null, null, null, "بوابة الموظفين (4)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 57, "REP-2026-004", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 5, 31, 35, 26, 97, 70, 25, null, 30, null, null, null, "الأرشفة الرقمية (5)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعريف", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 64, "REP-2026-005", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 6, 29, 78, 40, 90, 28, 44, null, 61, null, null, null, "نظام الأداء (6)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", null, new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 1, 3, 52, "REP-2026-006", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 7, 48, 64, 35, 70, 54, 23, null, 43, null, null, null, "خدمات المستفيدين (7)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 53, "REP-2026-007", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 8, 17, 99, 88, 77, 43, 69, null, 84, null, null, null, "منصة اعتماد (8)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "خدمي", null, new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 69, "REP-2026-008", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 9, 46, 32, 93, 85, 74, 49, null, 40, null, null, null, "نظام الاتصالات (9)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 84, "REP-2026-009", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 10, 37, 81, 43, 82, 99, 61, null, 71, null, null, null, "بوابة الموظفين (10)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تقني", null, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 74, "REP-2026-010", "توفير ميزانية إضافية للتدريب.", null, 1, null, null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 11, 92, 55, 71, 22, 51, 44, null, 49, null, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "الأرشفة الرقمية (11)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "تثبيت", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 48, "REP-2026-011", null, null, 4, new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 12, 29, 91, 76, 68, 90, 23, null, 57, null, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "منصة اعتماد (12)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "ثقافي", null, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 3, 9, 78, "REP-2026-012", null, null, 4, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 13, 53, 53, 60, 99, 88, 36, null, 44, null, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "تطوير الهيكل (13)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "تنظيمي", null, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهيئة", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 82, "REP-2026-013", null, null, 4, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
                    { 14, 40, 54, 81, 93, 74, 58, null, 56, null, null, "يرجى توضيح مؤشرات ADKAR بشكل أدق", "الدوام المرن (14)", "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.", "إجرائي", null, new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "تطبيق", null, "تم عقد 3 ورش عمل وإرسال نشرات توعوية.", null, null, null, "أغسطس", null, "تأخر تجاوب بعض الإدارات التقنية.", 2, 7, 82, "REP-2026-014", null, null, 4, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.", "صلاحيات الاعتماد ستبقى كما هي.", "يتماشى مع استراتيجية التحول الرقمي للجهة.", 2026 },
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

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 1 },
                    { 2, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 2 },
                    { 3, 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 3 },
                    { 4, 4, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 4 },
                    { 5, 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 5 },
                    { 6, 6, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 6 },
                    { 3, 7, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 7 },
                    { 4, 8, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 8 },
                    { 3, 9, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 9 },
                    { 7, 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 10 },
                    { 8, 11, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 11 },
                    { 9, 12, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 12 },
                    { 10, 13, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 13 }
                });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "ActionOwner", "ChairReviewNotes", "CreatedDate", "Duration", "ExpectedImpact", "GapType", "IncludeInExecutiveSummary", "IncludeInImpactDashboard", "IncludeInInstitutionalReport", "MonthlyReportId", "RecommendationNumber", "ReviewedDate", "Status", "SuccessIndicator", "SuggestedAction" },
                values: new object[,]
                {
                    { 36, "إدارة التواصل", null, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 36, "REC-036", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 37, "إدارة التواصل", null, new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 37, "REC-037", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 38, "إدارة التواصل", null, new DateTime(2026, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 38, "REC-038", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 39, "إدارة التواصل", null, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 39, "REC-039", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 40, "إدارة التواصل", null, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 40, "REC-040", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 41, "إدارة التواصل", null, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 41, "REC-041", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 42, "إدارة التواصل", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 42, "REC-042", null, 3, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 43, "إدارة التواصل", null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 43, "REC-043", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 44, "إدارة التواصل", null, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 44, "REC-044", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 45, "إدارة التواصل", null, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 45, "REC-045", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 46, "إدارة التواصل", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 46, "REC-046", new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 47, "إدارة التواصل", null, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 47, "REC-047", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 48, "إدارة التواصل", null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 48, "REC-048", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 49, "إدارة التواصل", null, new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 49, "REC-049", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." },
                    { 50, "إدارة التواصل", null, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 أسابيع", "تسريع تبني النظام الجديد", "مقاومة التغيير (Desire)", false, false, false, 50, "REC-050", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تجاوب 80% من المستهدفين", "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityInputs_OrganizationEntityId",
                table: "EntityInputs",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityInputs_PreparerId",
                table: "EntityInputs",
                column: "PreparerId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyReports_OrganizationEntityId",
                table: "MonthlyReports",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyReports_PreparerId",
                table: "MonthlyReports",
                column: "PreparerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_MonthlyReportId",
                table: "Recommendations",
                column: "MonthlyReportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationEntityId",
                table: "Users",
                column: "OrganizationEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "EntityInputs");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "MonthlyReports");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OrganizationEntities");
        }
    }
}
