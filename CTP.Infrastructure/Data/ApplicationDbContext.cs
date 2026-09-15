using CTP.Domain.Constants;
using CTP.Domain.Entities;
using CTP.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<ImpactMeasurement> ImpactMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ═══════ Foreign Keys ═══════
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User).WithMany()
                .HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<MonthlyReport>()
                .HasOne(m => m.Organization).WithMany()
                .HasForeignKey(m => m.OrganizationEntityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MonthlyReport>()
                .HasOne(m => m.Preparer).WithMany()
                .HasForeignKey(m => m.PreparerId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntityInput>()
                .HasOne(e => e.Organization).WithMany()
                .HasForeignKey(e => e.OrganizationEntityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EntityInput>()
                .HasOne(e => e.Preparer).WithMany()
                .HasForeignKey(e => e.PreparerId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.SourceReport).WithMany()
                .HasForeignKey(r => r.MonthlyReportId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImpactMeasurement>()
                .HasOne(im => im.Recommendation).WithMany()
                .HasForeignKey(im => im.RecommendationId).OnDelete(DeleteBehavior.Cascade);

            // ═══════ Fixed Values ═══════
            var fixedDate = new DateTime(2026, 1, 1);
            const string defaultPassword = "$2a$12$2MFOVCFQZyT5u8uzANHEkexgxCHk9NijF4NLseoeV5321.mAEvwwy"; // 123456

            // ═══════ 1. Roles ═══════
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleCode = AppRoles.Staff, RoleName = "المنسوبون", CreatedDate = fixedDate },
                new Role { RoleId = 2, RoleCode = AppRoles.Manager, RoleName = "القادة والمدراء المباشرون", CreatedDate = fixedDate },
                new Role { RoleId = 3, RoleCode = AppRoles.Ambassador, RoleName = "سفراء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 4, RoleCode = AppRoles.UnitLeader, RoleName = "قادة الوحدات والهيئات والإدارات", CreatedDate = fixedDate },
                new Role { RoleId = 5, RoleCode = AppRoles.CommitteeMember, RoleName = "أعضاء لجنة شركاء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 6, RoleCode = AppRoles.CommitteeChair, RoleName = "رئيس لجنة شركاء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 7, RoleCode = AppRoles.ChiefOfStaff, RoleName = "رئيس فريق عمل القائد", CreatedDate = fixedDate },
                new Role { RoleId = 8, RoleCode = AppRoles.DeputyLeader, RoleName = "سعادة نائب القائد", CreatedDate = fixedDate },
                new Role { RoleId = 9, RoleCode = AppRoles.Leader, RoleName = "معالي القائد", CreatedDate = fixedDate },
                new Role { RoleId = 10, RoleCode = AppRoles.CorporateComms, RoleName = "الاتصال المؤسسي", CreatedDate = fixedDate }
            );

            // ═══════ 2. Entities (29 جهة) ═══════
            var entityData = new (string Code, string Name, string Rank)[]
            {
                ("ORG-001", "مكتب معالي القائد", "لواء"),
                ("ORG-002", "مكتب نائب القائد", "لواء"),
                ("ORG-003", "فريق عمل القائد", "لواء"),
                ("ORG-004", "إدارة شؤون الضباط", "لواء"),
                ("ORG-005", "إدارة التدريب", "لواء"),
                ("ORG-006", "إدارة التفتيش والتقييم", "لواء"),
                ("ORG-007", "إدارة العلاقات العامة والتوجيه المعنوي", "عميد"),
                ("ORG-008", "إدارة الشؤون الإدارية والمالية", "لواء"),
                ("ORG-009", "إدارة الشؤون الدينية", "لواء"),
                ("ORG-010", "إدارة المشاريع", "لواء"),
                ("ORG-011", "الإدارة الهندسية والأشغال", "لواء"),
                ("ORG-012", "إدارة الشرطة العسكرية", "عميد"),
                ("ORG-013", "إدارة الاتصالات وتقنية المعلومات", "لواء"),
                ("ORG-014", "هيئة الإدارة", "لواء"),
                ("ORG-015", "هيئة العمليات", "لواء"),
                ("ORG-016", "هيئة التخطيط والميزانية لبناء قوات الدفاع الجوي", "لواء"),
                ("ORG-017", "هيئة الاستخبارات والأمن", "لواء"),
                ("ORG-018", "هيئة الإمدادات والتموين", "لواء"),
                ("ORG-019", "هيئة التخطيط الاستراتيجي", "لواء"),
                ("ORG-020", "مجموعة الدفاع الجوي الأولى", "لواء"),
                ("ORG-021", "مجموعة الدفاع الجوي الثانية", "لواء"),
                ("ORG-022", "مجموعة الدفاع الجوي الثالثة", "لواء"),
                ("ORG-023", "مجموعة الدفاع الجوي الرابعة", "لواء"),
                ("ORG-024", "مجموعة الدفاع الجوي الخامسة", "لواء"),
                ("ORG-025", "مجموعة الدفاع الجوي السادسة", "لواء"),
                ("ORG-026", "كلية الملك عبدالله للدفاع الجوي", "لواء"),
                ("ORG-027", "معهد قوات الدفاع الجوي", "لواء"),
                ("ORG-028", "مركز ومدرسة قوات الدفاع الجوي", "عميد"),
                ("ORG-029", "قاعدة الصيانة والإسناد الفني", "لواء")
            };

            var entities = entityData.Select((e, i) => new OrganizationEntity
            {
                Id = i + 1,
                EntityCode = e.Code,
                EntityName = e.Name,
                IsActive = true,
                CreatedDate = fixedDate
            }).ToList();

            modelBuilder.Entity<OrganizationEntity>().HasData(entities);

            // ═══════ 3. Committee ═══════
            modelBuilder.Entity<Committee>().HasData(
                new Committee { Id = 1, Name = "لجنة شركاء التغيير", Description = "اللجنة المركزية لإدارة وقياس أثر التغيير", IsActive = true, CreatedDate = fixedDate }
            );

            // ═══════ 4. Users ═══════
            var users = new List<User>();
            var userRoles = new List<UserRole>();
            int urCounter = 1;

            // --- القيادة الثابتة ---
            users.Add(new User { UserId = 1, Username = "leader", PasswordHash = defaultPassword, FullName = "معالي قائد قوات الدفاع الجوي", JobTitle = "معالي القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 1, RoleId = 9, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 2, Username = "deputy", PasswordHash = defaultPassword, FullName = "سعادة نائب قائد قوات الدفاع الجوي", JobTitle = "سعادة نائب القائد", OrganizationEntityId = 2, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 2, RoleId = 8, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 3, Username = "chief", PasswordHash = defaultPassword, FullName = "رئيس فريق عمل القائد", JobTitle = "رئيس فريق عمل القائد", OrganizationEntityId = 3, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 3, RoleId = 7, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 4, Username = "chair", PasswordHash = defaultPassword, FullName = "رئيس لجنة شركاء التغيير", JobTitle = "رئيس لجنة شركاء التغيير", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 4, RoleId = 6, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 5, Username = "member1", PasswordHash = defaultPassword, FullName = "عضو لجنة شركاء التغيير (1)", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 5, RoleId = 5, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 6, Username = "member2", PasswordHash = defaultPassword, FullName = "عضو لجنة شركاء التغيير (2)", OrganizationEntityId = 15, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 6, RoleId = 5, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 7, Username = "member3", PasswordHash = defaultPassword, FullName = "عضو لجنة شركاء التغيير (3)", OrganizationEntityId = 20, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 7, RoleId = 5, AssignedDate = fixedDate, IsActive = true });

            users.Add(new User { UserId = 8, Username = "comms", PasswordHash = defaultPassword, FullName = "مسؤول الاتصال المؤسسي", JobTitle = "الاتصال المؤسسي", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate });
            userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = 8, RoleId = 10, AssignedDate = fixedDate, IsActive = true });

            // --- مستخدمو الجهات (3 لكل جهة) ---
            int userId = 100;
            for (int i = 0; i < entities.Count; i++)
            {
                var e = entities[i];
                var rank = entityData[i].Rank;
                var n = i + 1;

                // قائد الوحدة
                users.Add(new User { UserId = userId++, Username = $"unitleader{n}", PasswordHash = defaultPassword, FullName = $"{rank} / قائد {e.EntityName}", JobTitle = $"{rank} — قائد {e.EntityName}", OrganizationEntityId = e.Id, IsActive = true, CreatedDate = fixedDate });
                userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = userId - 1, RoleId = 4, AssignedDate = fixedDate, IsActive = true });

                // السفير
                users.Add(new User { UserId = userId++, Username = $"ambassador{n}", PasswordHash = defaultPassword, FullName = $"سفير التغيير — {e.EntityName}", JobTitle = $"سفير التغيير في {e.EntityName}", OrganizationEntityId = e.Id, IsActive = true, CreatedDate = fixedDate });
                userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = userId - 1, RoleId = 3, AssignedDate = fixedDate, IsActive = true });

                // منسوب
                users.Add(new User { UserId = userId++, Username = $"staff{n}", PasswordHash = defaultPassword, FullName = $"منسوب — {e.EntityName}", OrganizationEntityId = e.Id, IsActive = true, CreatedDate = fixedDate });
                userRoles.Add(new UserRole { UserRoleId = urCounter++, UserId = userId - 1, RoleId = 1, AssignedDate = fixedDate, IsActive = true });
            }

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserRole>().HasData(userRoles);

            // ═══════ 5. Reports & Recommendations ═══════
            var rand = new Random(2026);
            var changeNames = new[]
            {
                "توحيد التقارير الشهرية عبر المنصة",
                "تحديث إجراءات الخدمات الداخلية",
                "التحول إلى المراسلات الرقمية",
                "برنامج تطوير قدرات القيادات الوسطى",
                "منصة إدارة الوثائق الإلكترونية",
                "نظام تتبع المبادرات الاستراتيجية",
                "تطبيق إدارة الأداء المؤسسي",
                "بوابة الخدمات الذاتية للمنسوبين",
                "منصة التدريب الإلكتروني",
                "نظام الاجتماعات الافتراضية",
                "أتمتة إجراءات الصيانة",
                "منصة البلاغات الذكية"
            };
            var changeTypes = new[] { "تنظيمي", "إجرائي", "تقني", "خدمي", "ثقافي" };
            var stages = new[] { "تعريف", "تهيئة", "تطبيق", "تثبيت" };
            var months = new[] { "محرم", "صفر", "ربيع الأول", "ربيع الآخر", "جمادى الأولى", "جمادى الآخرة", "رجب", "شعبان", "رمضان", "شوال", "ذو القعدة", "ذو الحجة" };

            // توزيع الحالات: كل 7 تقارير دورة كاملة
            var statusCycle = new[]
            {
                ReportStatus.Draft, ReportStatus.Submitted, ReportStatus.Returned,
                ReportStatus.UnderAnalysis, ReportStatus.UnderAnalysis,
                ReportStatus.UnderAnalysis, ReportStatus.Approved
            };

            var reports = new List<MonthlyReport>();
            var recommendations = new List<Recommendation>();
            int reportId = 1;
            int recId = 1;

            for (int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var ambassadorId = 100 + (i * 3);       // ambassadorN
                var unitleaderId = 100 + (i * 3) + 1;   // unitleaderN

                // تقريران لكل جهة
                for (int r = 0; r < 2; r++)
                {
                    var status = statusCycle[(reportId - 1) % statusCycle.Length];
                    var changeIdx = (i + r) % changeNames.Length;

                    var adkarAwareness = rand.Next(35, 95);
                    var adkarDesire = rand.Next(35, 95);
                    var adkarKnowledge = rand.Next(35, 95);
                    var adkarAbility = rand.Next(30, 90);
                    var adkarReinforcement = rand.Next(25, 85);
                    var readiness = (adkarAwareness + adkarDesire + adkarKnowledge) / 3;
                    var adoption = (adkarAbility + adkarReinforcement) / 2;
                    var stageIdx = (i + r) % stages.Length;
                    var stageDesc = stageIdx switch
                    {
                        0 => "التعريفية",
                        1 => "التهيئة",
                        2 => "التطبيق الميداني",
                        _ => "التثبيت والاستدامة"
                    };
                    var report = new MonthlyReport
                    {
                        Id = reportId,
                        ReportNumber = $"CTP-2026-{(reportId):D3}",
                        Year = 2026,
                        Month = months[(i + r) % 12],
                        OrganizationEntityId = entity.Id,
                        PreparerId = ambassadorId,
                        ChangeName = changeNames[changeIdx],
                        ChangeType = changeTypes[(i + r) % changeTypes.Length],
                        CurrentStage = stages[(i + r) % stages.Length],
                        AffectedGroups = $"منسوبو {entity.EntityName}",
                        ImpactScope = "الجهة",
                        MostInNeedGroup = "المنسوبون الجدد والفئات الأقل خبرة",
                        ActivityCompletionRate = rand.Next(40, 95),
                        ChangeSummary = $"اعتماد {changeNames[changeIdx]} كجزء من مبادرات التحول في {entity.EntityName} ضمن المرحلة {stageDesc}.",
                        WhyImportant = $"يسهم {changeNames[changeIdx]} في رفع كفاءة العمليات وتحسين تجربة المنسوبين والمستفيدين، تحقيقاً لمستهدفات رؤية المملكة 2030.",
                        WhatWillChange = $"الانتقال من الإجراءات اليدوية الحالية إلى نمط عمل محسّن يعكس أفضل الممارسات في مجال {changeTypes[(i + r) % changeTypes.Length]}.",
                        WhatWillNotChange = "الصلاحيات النظامية ومسارات الاعتماد الرسمية ستبقى كما هي دون تغيير.",
                        ExecutedActivities = "تم عقد ورش عمل تعريفية، وإرسال نشرات توعوية، واجتماعات مع القادة والمدراء المباشرين.",
                        AdkarAwareness = adkarAwareness,
                        AdkarDesire = adkarDesire,
                        AdkarKnowledge = adkarKnowledge,
                        AdkarAbility = adkarAbility,
                        AdkarReinforcement = adkarReinforcement,
                        ReadinessScore = readiness,
                        AdoptionScore = adoption,
                        Obstacles = "ضغط الأعباء التشغيلية اليومية أدى إلى تباطؤ التفاعل، مع حاجة بعض الفئات لمزيد من التوضيح.",
                        AdoptionBarriers = "الاعتياد على الإجراءات السابقة، والحاجة إلى تدريب عملي إضافي للفئة الأقل خبرة.",
                        RequiredSupport = "نحتاج جلسة توعوية إضافية للفريق، ودعماً تدريبياً، ومواد توعوية مبسطة.",
                        Risks = "تأخر التبني في حال عدم معالجة العوائق خلال شهر",
                        ImprovementOpportunities = "تبسيط النموذج، وإتاحة أمثلة إرشادية",
                        SuccessStories = "تفاعل إيجابي من الفريق خلال الجلسات التعريفية",
                        InitialRecommendation = "تكثيف الجلسات التعريفية للفئات الأقل جاهزية مع جدولة تدريب عملي مصغر.",
                        Status = status,
                        CreatedDate = fixedDate.AddDays(-(reportId % 25) - 5),
                        SubmittedDate = status != ReportStatus.Draft ? fixedDate.AddDays(-(reportId % 15) - 2) : null,
                        ApprovedDate = status == ReportStatus.Approved ? fixedDate.AddDays(-(reportId % 5)) : null,
                        ApproverNotes = status == ReportStatus.Returned ? "يرجى توضيح مؤشرات ADKAR بشكل أدق وإرفاق الشواهد." : null
                    };
                    reports.Add(report);

                    // Recommendation للتقارير المعتمدة (Approved) أو تحت التحليل (UnderAnalysis)
                    if (status == ReportStatus.Approved || (status == ReportStatus.UnderAnalysis && reportId % 2 == 0))
                    {
                        var recStatus = status == ReportStatus.Approved
                            ? RecommendationStatus.Approved
                            : (reportId % 4 == 0 ? RecommendationStatus.ReadyForChair : RecommendationStatus.AwaitingSupport);

                        var reviewedDate = status == ReportStatus.Approved
                            ? (DateTime?)fixedDate.AddDays(-(reportId % 4))
                            : null;

                        recommendations.Add(new Recommendation
                        {
                            Id = recId,
                            RecommendationNumber = $"REC-{recId:D3}",
                            MonthlyReportId = reportId,
                            GapType = new[] { "فجوة وعي", "فجوة رغبة", "فجوة معرفة", "فجوة قدرة", "فجوة تعزيز" }[recId % 5],
                            SuggestedAction = $"تنفيذ حزمة دعم موجهة (توعية + تدريب مصغر) للفئة المتأثرة في {entity.EntityName} خلال 4 أسابيع.",
                            ActionOwner = $"ممثل {entity.EntityName}",
                            Duration = "4 أسابيع",
                            SuccessIndicator = "ارتفاع مؤشر التبني 8 نقاط",
                            ExpectedImpact = "إغلاق الفجوة ورفع مستوى التبني",
                            Evidence = $"مؤشر {new[] { "الوعي", "الرغبة", "المعرفة", "القدرة", "التعزيز" }[recId % 5]} أقل من المستهدف",
                            Cause = "قصور وصول المحتوى التوضيحي والدعم للفئة المتأثرة",
                            GapEffect = "إبطاء الانتقال بين مراحل التغيير وخفض التبني",
                            ImpactMeasure = "مقارنة قبل/بعد في التقرير الشهري التالي",
                            SupportDecision = recStatus == RecommendationStatus.AwaitingSupport ? "يحتاج قرار دعم قيادي" : "لا يتطلب قرار دعم إضافي",
                            Escalation = recStatus == RecommendationStatus.AwaitingSupport ? "سعادة النائب عند الحاجة" : "سعادة رئيس فريق عمل القائد",
                            Status = recStatus,
                            IncludeInInstitutionalReport = recStatus == RecommendationStatus.Approved,
                            IncludeInImpactDashboard = recStatus == RecommendationStatus.Approved && recId % 2 == 0,
                            IncludeInExecutiveSummary = recStatus == RecommendationStatus.Approved && recId % 3 == 0,
                            ChairReviewNotes = recStatus == RecommendationStatus.Approved ? "اعتُمدت — يُتابع التنفيذ عبر المسار الرسمي." : null,
                            ReviewedDate = reviewedDate,
                            CreatedDate = fixedDate.AddDays(-(reportId % 20))
                        });
                        recId++;
                    }

                    reportId++;
                }
            }

            modelBuilder.Entity<MonthlyReport>().HasData(reports);
            modelBuilder.Entity<Recommendation>().HasData(recommendations);

            // ═══════ 6. Impact Measurements (عينة) ═══════
            var approvedRecs = recommendations
                .Where(r => r.Status == RecommendationStatus.Approved && r.IncludeInImpactDashboard)
                .Take(5)
                .ToList();

            var impactMeasurements = new List<ImpactMeasurement>();
            int impactId = 1;
            foreach (var rec in approvedRecs)
            {
                var before = rand.Next(35, 55);
                var after = before + rand.Next(5, 20);
                impactMeasurements.Add(new ImpactMeasurement
                {
                    Id = impactId++,
                    RecommendationId = rec.Id,
                    IndicatorName = rec.SuccessIndicator,
                    BeforeValue = before,
                    AfterValue = after,
                    Status = (after - before) >= 10 ? "تحقق" : "جزئي",
                    NextStep = "استمرار المتابعة الشهرية",
                    Lesson = (after - before) >= 10 ? "التدخل المبكر مع الفئة المتأثرة يرفع المؤشر بسرعة" : "بعض المؤشرات تحتاج دورتي قياس",
                    CreatedDate = fixedDate.AddDays(-5),
                    MeasuredDate = fixedDate.AddDays(-1)
                });
            }
            modelBuilder.Entity<ImpactMeasurement>().HasData(impactMeasurements);

            // ═══════ 7. Entity Inputs (عينة) ═══════
            var entityInputs = new List<EntityInput>();
            var inputTypes = new[]
            {
                InputType.Challenge, InputType.SuccessStory, InputType.Improvement,
                InputType.Inquiry, InputType.SupportNeed, InputType.PlatformFeedback, InputType.AdoptionBarrier
            };
            var inputContents = new[]
            {
                "تأخر الاستجابة من الدعم الفني يعيق التقدم في المبادرة.",
                "انخفاض معدل التسرب الوظيفي بنسبة 15% بعد تطبيق النظام الجديد.",
                "نقترح إضافة ميزة التوقيع الإلكتروني المتعدد لتبسيط الإجراءات.",
                "هل سيتم ربط النظام الجديد بمنصة اعتماد خلال الربع القادم؟",
                "نحتاج ميزانية إضافية لتدريب 50 موظفاً على الأدوات الجديدة.",
                "واجهة المستخدم في شاشات التقارير بطيئة التجاوب مع البيانات الكبيرة.",
                "مقاومة تغيير من الموظفين القدامى تجاه الإجراءات الجديدة."
            };
            for (int i = 0; i < 30; i++)
            {
                var entityIdx = i % entities.Count;
                var entity = entities[entityIdx];
                var preparerId = 100 + (entityIdx * 3); // السفير
                entityInputs.Add(new EntityInput
                {
                    Id = i + 1,
                    OrganizationEntityId = entity.Id,
                    PreparerId = preparerId,
                    Type = inputTypes[i % inputTypes.Length],
                    Content = inputContents[i % inputContents.Length],
                    Status = i % 3 == 0 ? InputStatus.Received : (InputStatus)((i % 4) + 2),
                    CreatedDate = fixedDate.AddDays(-(i % 30) - 2)
                });
            }
            modelBuilder.Entity<EntityInput>().HasData(entityInputs);
        }
    }
}