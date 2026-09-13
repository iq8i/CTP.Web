using CTP.Domain.Constants;
using CTP.Domain.Entities;
using CTP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTP.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OrganizationEntity> OrganizationEntities { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<EntityInput> EntityInputs { get; set; }
        public DbSet<MonthlyReport> MonthlyReports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>().HasOne(n => n.User).WithMany().HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
            modelBuilder.Entity<MonthlyReport>().HasOne(m => m.Organization).WithMany().HasForeignKey(m => m.OrganizationEntityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MonthlyReport>().HasOne(m => m.Preparer).WithMany().HasForeignKey(m => m.PreparerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EntityInput>().HasOne(e => e.Organization).WithMany().HasForeignKey(e => e.OrganizationEntityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EntityInput>().HasOne(e => e.Preparer).WithMany().HasForeignKey(e => e.PreparerId).OnDelete(DeleteBehavior.Restrict);

            var fixedDate = new DateTime(2026, 8, 1);
            var defaultPassword = "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy";

            // 1. الأساسيات (الجهات واللجان والأدوار)
            modelBuilder.Entity<OrganizationEntity>().HasData(
                new OrganizationEntity { Id = 1, EntityCode = "UNIT-01", EntityName = "وحدة التحول الرقمي", IsActive = true, CreatedDate = fixedDate },
                new OrganizationEntity { Id = 2, EntityCode = "UNIT-02", EntityName = "إدارة الموارد البشرية", IsActive = true, CreatedDate = fixedDate },
                new OrganizationEntity { Id = 3, EntityCode = "UNIT-03", EntityName = "قطاع العمليات", IsActive = true, CreatedDate = fixedDate }
            );

            modelBuilder.Entity<Committee>().HasData(new Committee { Id = 1, Name = "لجنة شركاء التغيير", IsActive = true, CreatedDate = fixedDate });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleCode = AppRoles.Staff, RoleName = "المنسوبون", CreatedDate = fixedDate },
                new Role { RoleId = 2, RoleCode = AppRoles.Manager, RoleName = "القادة والمدراء المباشرون", CreatedDate = fixedDate },
                new Role { RoleId = 3, RoleCode = AppRoles.Ambassador, RoleName = "سفراء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 4, RoleCode = AppRoles.UnitLeader, RoleName = "قادة الوحدات", CreatedDate = fixedDate },
                new Role { RoleId = 5, RoleCode = AppRoles.CommitteeMember, RoleName = "أعضاء اللجنة", CreatedDate = fixedDate },
                new Role { RoleId = 6, RoleCode = AppRoles.CommitteeChair, RoleName = "رئيس اللجنة", CreatedDate = fixedDate },
                new Role { RoleId = 7, RoleCode = AppRoles.ChiefOfStaff, RoleName = "رئيس فريق عمل القائد", CreatedDate = fixedDate },
                new Role { RoleId = 8, RoleCode = AppRoles.DeputyLeader, RoleName = "نائب القائد", CreatedDate = fixedDate },
                new Role { RoleId = 9, RoleCode = AppRoles.Leader, RoleName = "القائد", CreatedDate = fixedDate },
                new Role { RoleId = 10, RoleCode = AppRoles.CorporateComms, RoleName = "الاتصال المؤسسي", CreatedDate = fixedDate }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "staff", PasswordHash = defaultPassword, FullName = "مستخدم منسوب", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 2, Username = "manager", PasswordHash = defaultPassword, FullName = "مدير مباشر", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 3, Username = "ambassador1", PasswordHash = defaultPassword, FullName = "سفير التغيير 1", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 4, Username = "unitleader", PasswordHash = defaultPassword, FullName = "قائد وحدة 1", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 5, Username = "member", PasswordHash = defaultPassword, FullName = "عضو اللجنة", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 6, Username = "chair", PasswordHash = defaultPassword, FullName = "رئيس اللجنة", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 7, Username = "ambassador2", PasswordHash = defaultPassword, FullName = "سفير التغيير 2", OrganizationEntityId = 2, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 8, Username = "unitleader2", PasswordHash = defaultPassword, FullName = "قائد وحدة 2", OrganizationEntityId = 2, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 9, Username = "ambassador3", PasswordHash = defaultPassword, FullName = "سفير التغيير 3", OrganizationEntityId = 3, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 10, Username = "chief", PasswordHash = defaultPassword, FullName = "رئيس فريق عمل القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 11, Username = "deputy", PasswordHash = defaultPassword, FullName = "نائب القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 12, Username = "leader", PasswordHash = defaultPassword, FullName = "معالي القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 13, Username = "comms", PasswordHash = defaultPassword, FullName = "الاتصال المؤسسي", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId = 1, UserId = 1, RoleId = 1, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 2, UserId = 2, RoleId = 2, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 3, UserId = 3, RoleId = 3, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 4, UserId = 4, RoleId = 4, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 5, UserId = 5, RoleId = 5, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 6, UserId = 6, RoleId = 6, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 7, UserId = 7, RoleId = 3, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 8, UserId = 8, RoleId = 4, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 9, UserId = 9, RoleId = 3, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 10, UserId = 10, RoleId = 7, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 11, UserId = 11, RoleId = 8, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 12, UserId = 12, RoleId = 9, AssignedDate = fixedDate },
                new UserRole { UserRoleId = 13, UserId = 13, RoleId = 10, AssignedDate = fixedDate }
            );

            // ============================================================
            // محرك توليد البيانات الضخمة (Deterministic Data Generator)
            // ============================================================
            var rand = new Random(2026); // Seed ثابت لضمان استقرار ملفات التهجير (Migrations)
            string[] changeNames = { "نظام الاتصالات", "منصة اعتماد", "تطوير الهيكل", "بوابة الموظفين", "الدوام المرن", "أتمتة المشتريات", "نظام الأداء", "الأرشفة الرقمية", "خدمات المستفيدين", "ترقية الخوادم" };
            string[] stages = { "تعريف", "تهيئة", "تطبيق", "تثبيت" };
            string[] types = { "تقني", "تنظيمي", "إجرائي", "ثقافي", "خدمي" };
            int[] preparers = { 3, 7, 9 };

            var reports = new List<MonthlyReport>();
            var recommendations = new List<Recommendation>();

            // توليد 50 تقرير لتغطية جميع المسارات
            for (int i = 1; i <= 50; i++)
            {
                int adA = rand.Next(20, 100); int adD = rand.Next(20, 100); int adK = rand.Next(20, 100); int adAb = rand.Next(20, 100); int adR = rand.Next(20, 100);
                int preparerId = preparers[rand.Next(preparers.Length)];
                int orgId = preparerId == 3 ? 1 : (preparerId == 7 ? 2 : 3);

                ReportStatus status;
                if (i <= 10) status = ReportStatus.Draft;                    // 10 مسودات للسفراء
                else if (i <= 15) status = ReportStatus.Returned;             // 5 معادة للسفراء
                else if (i <= 25) status = ReportStatus.Submitted;            // 10 لدى قادة الوحدات
                else if (i <= 35) status = ReportStatus.UnderAnalysis;        // 10 في صندوق وارد اللجنة (بدون توصيات)
                else if (i <= 42) status = ReportStatus.UnderAnalysis;        // 7 لدى رئيس اللجنة (لها توصيات مبدئية)
                else status = ReportStatus.Approved;                          // 8 في الأرشيف (معتمدة نهائياً)

                var report = new MonthlyReport
                {
                    Id = i,
                    ReportNumber = $"REP-2026-{i:D3}",
                    Year = 2026,
                    Month = "أغسطس",
                    OrganizationEntityId = orgId,
                    PreparerId = preparerId,
                    ChangeName = changeNames[rand.Next(changeNames.Length)] + $" ({i})",
                    ChangeType = types[rand.Next(types.Length)],
                    CurrentStage = stages[rand.Next(stages.Length)],
                    ActivityCompletionRate = rand.Next(10, 100),
                    AdkarAwareness = adA,
                    AdkarDesire = adD,
                    AdkarKnowledge = adK,
                    AdkarAbility = adAb,
                    AdkarReinforcement = adR,
                    ReadinessScore = (adA + adD + adK) / 3,
                    AdoptionScore = (adAb + adR) / 2,
                    ChangeSummary = "ملخص تفصيلي عن التغيير والأهداف الاستراتيجية المرتبطة به.",
                    WhyImportant = "يتماشى مع استراتيجية التحول الرقمي للجهة.",
                    WhatWillChange = "العمليات اليدوية ستتحول إلى مؤتمتة بالكامل.",
                    WhatWillNotChange = "صلاحيات الاعتماد ستبقى كما هي.",
                    ExecutedActivities = "تم عقد 3 ورش عمل وإرسال نشرات توعوية.",
                    Obstacles = "تأخر تجاوب بعض الإدارات التقنية.",
                    RequiredSupport = status == ReportStatus.Returned ? null : "توفير ميزانية إضافية للتدريب.",
                    Status = status,
                    CreatedDate = fixedDate.AddDays(-rand.Next(1, 30)),
                    SubmittedDate = status != ReportStatus.Draft ? fixedDate.AddDays(-rand.Next(1, 15)) : null,
                    ApprovedDate = status == ReportStatus.Approved ? fixedDate.AddDays(-rand.Next(1, 5)) : null,
                    ApproverNotes = status == ReportStatus.Returned ? "يرجى توضيح مؤشرات ADKAR بشكل أدق" : null
                };
                reports.Add(report);

                // توليد توصيات للتقارير المخصصة لرئيس اللجنة (36-42) والأرشيف (43-50)
                if (i >= 36)
                {
                    CTP.Domain.Enums.RecommendationStatus recStatus = i <= 42
    ? CTP.Domain.Enums.RecommendationStatus.ReadyForChair
    : CTP.Domain.Enums.RecommendationStatus.Approved;
                    recommendations.Add(new Recommendation
                    {
                        Id = i, // استخدام نفس الـ ID للتسهيل
                        RecommendationNumber = $"REC-{i:D3}",
                        MonthlyReportId = i,
                        GapType = "مقاومة التغيير (Desire)",
                        SuggestedAction = "عقد اجتماعات مكثفة مع الموظفين المتأثرين وتوضيح المكاسب.",
                        ActionOwner = "إدارة التواصل",
                        Duration = "3 أسابيع",
                        SuccessIndicator = "تجاوب 80% من المستهدفين",
                        ExpectedImpact = "تسريع تبني النظام الجديد",
                        Status = recStatus,
                        CreatedDate = report.CreatedDate.AddDays(1),
                        ReviewedDate = recStatus == CTP.Domain.Enums.RecommendationStatus.Approved
    ? report.ApprovedDate
    : null
                    });
                }
            }

            modelBuilder.Entity<MonthlyReport>().HasData(reports);
            modelBuilder.Entity<Recommendation>().HasData(recommendations);

            // توليد 20 مدخل تفاعلي (Challenges, Success Stories, etc.)
            var inputs = new List<EntityInput>();
            for (int i = 1; i <= 20; i++)
            {
                inputs.Add(new EntityInput
                {
                    Id = i,
                    OrganizationEntityId = (i % 3) + 1,
                    PreparerId = preparers[i % preparers.Length],
                    Type = (InputType)(rand.Next(1, 8)),
                    Content = $"هذا النص يمثل محتوى المدخل رقم {i}، ويحتوي على تفاصيل العائق أو قصة النجاح المرصودة في الميدان.",
                    Status = (InputStatus)(rand.Next(1, 6)),
                    CreatedDate = fixedDate.AddDays(-rand.Next(1, 60))
                });
            }
            modelBuilder.Entity<EntityInput>().HasData(inputs);
        }
    }
}