using CTP.Domain.Constants;
using CTP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CTP.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // جداول النظام الأساسية
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
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // إعداد المفتاح المركب والعلاقات
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);

            // منع الحذف المتسلسل
            modelBuilder.Entity<MonthlyReport>()
                .HasOne(m => m.Organization).WithMany().HasForeignKey(m => m.OrganizationEntityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MonthlyReport>()
                .HasOne(m => m.Preparer).WithMany().HasForeignKey(m => m.PreparerId).OnDelete(DeleteBehavior.Restrict);

            var fixedDate = new DateTime(2026, 1, 1);

            // 1. بذر الجهة التنظيمية الأساسية
            modelBuilder.Entity<OrganizationEntity>().HasData(
                new OrganizationEntity { Id = 1, EntityCode = "UNIT-01", EntityName = "وحدة التحول الرقمي", IsActive = true, CreatedDate = fixedDate }
            );

            // 2. بذر الأدوار العشرة
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleCode = CTP.Domain.Constants.AppRoles.Staff, RoleName = "المنسوبون", CreatedDate = fixedDate },
                new Role { RoleId = 2, RoleCode = CTP.Domain.Constants.AppRoles.Manager, RoleName = "القادة والمدراء المباشرون", CreatedDate = fixedDate },
                new Role { RoleId = 3, RoleCode = CTP.Domain.Constants.AppRoles.Ambassador, RoleName = "سفراء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 4, RoleCode = CTP.Domain.Constants.AppRoles.UnitLeader, RoleName = "أصحاب السعادة قادة الوحدات والمدراء", CreatedDate = fixedDate },
                new Role { RoleId = 5, RoleCode = CTP.Domain.Constants.AppRoles.CommitteeMember, RoleName = "أعضاء لجنة شركاء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 6, RoleCode = CTP.Domain.Constants.AppRoles.CommitteeChair, RoleName = "رئيس لجنة شركاء التغيير", CreatedDate = fixedDate },
                new Role { RoleId = 7, RoleCode = CTP.Domain.Constants.AppRoles.ChiefOfStaff, RoleName = "سعادة رئيس فريق عمل القائد", CreatedDate = fixedDate },
                new Role { RoleId = 8, RoleCode = CTP.Domain.Constants.AppRoles.DeputyLeader, RoleName = "سعادة نائب القائد", CreatedDate = fixedDate },
                new Role { RoleId = 9, RoleCode = CTP.Domain.Constants.AppRoles.Leader, RoleName = "معالي القائد", CreatedDate = fixedDate },
                new Role { RoleId = 10, RoleCode = CTP.Domain.Constants.AppRoles.CorporateComms, RoleName = "الاتصال المؤسسي", CreatedDate = fixedDate }
            );

            // 3. بذر 10 مستخدمين (كلمة المرور الموحدة: 123456 لبيئة التدشين)
            var defaultPassword = "123456";
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "staff", PasswordHash = defaultPassword, FullName = "مستخدم منسوب", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 2, Username = "manager", PasswordHash = defaultPassword, FullName = "مدير مباشر", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 3, Username = "ambassador", PasswordHash = defaultPassword, FullName = "سفير التغيير", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 4, Username = "unitleader", PasswordHash = defaultPassword, FullName = "قائد الوحدة", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 5, Username = "member", PasswordHash = defaultPassword, FullName = "عضو اللجنة", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 6, Username = "chair", PasswordHash = defaultPassword, FullName = "رئيس اللجنة", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 7, Username = "chief", PasswordHash = defaultPassword, FullName = "رئيس فريق عمل القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 8, Username = "deputy", PasswordHash = defaultPassword, FullName = "نائب القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 9, Username = "leader", PasswordHash = defaultPassword, FullName = "معالي القائد", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate },
                new User { UserId = 10, Username = "comms", PasswordHash = defaultPassword, FullName = "موظف الاتصال", OrganizationEntityId = 1, IsActive = true, CreatedDate = fixedDate }
            );

            // 4. ربط كل مستخدم بدوره المقابل
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId = 1, UserId = 1, RoleId = 1, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 2, UserId = 2, RoleId = 2, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 3, UserId = 3, RoleId = 3, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 4, UserId = 4, RoleId = 4, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 5, UserId = 5, RoleId = 5, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 6, UserId = 6, RoleId = 6, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 7, UserId = 7, RoleId = 7, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 8, UserId = 8, RoleId = 8, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 9, UserId = 9, RoleId = 9, AssignedDate = fixedDate, IsActive = true },
                new UserRole { UserRoleId = 10, UserId = 10, RoleId = 10, AssignedDate = fixedDate, IsActive = true }
            );

        }
    }
}