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

        public DbSet<MonthlyReport> MonthlyReports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // إعداد المفتاح المركب والعلاقات
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);

            // منع الحذف المتسلسل للتقارير لحماية سلامة البيانات (تمت إضافتها في الخطوة السابقة)
            modelBuilder.Entity<MonthlyReport>().HasOne(m => m.Organization).WithMany().HasForeignKey(m => m.OrganizationEntityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MonthlyReport>().HasOne(m => m.Preparer).WithMany().HasForeignKey(m => m.PreparerId).OnDelete(DeleteBehavior.Restrict);

            // 1. بذر الأدوار الأساسية
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleCode = "LEADER", RoleName = "معالي القائد", CreatedDate = new DateTime(2026, 1, 1) },
                new Role { RoleId = 2, RoleCode = "AMBASSADOR", RoleName = "سفير التغيير", CreatedDate = new DateTime(2026, 1, 1) },
                new Role { RoleId = 3, RoleCode = "COMMITTEE_CHAIR", RoleName = "رئيس لجنة شركاء التغيير", CreatedDate = new DateTime(2026, 1, 1) },
                new Role { RoleId = 4, RoleCode = "STAFF", RoleName = "المنسوبون", CreatedDate = new DateTime(2026, 1, 1) }
            );

            // 2. بذر جهة تجريبية (Organization) لربط المستخدمين والتقارير
            modelBuilder.Entity<OrganizationEntity>().HasData(
                new OrganizationEntity { Id = 1, EntityCode = "UNIT-01", EntityName = "وحدة التحول الرقمي", IsActive = true, CreatedDate = new DateTime(2026, 1, 1) }
            );

            // 3. بذر المستخدمين (المدير + السفير) وربطهم بالجهة رقم 1
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", PasswordHash = "123456", FullName = "مدير النظام التجريبي", OrganizationEntityId = 1, IsActive = true, CreatedDate = new DateTime(2026, 1, 1) },
                new User { UserId = 2, Username = "ambassador", PasswordHash = "123456", FullName = "سفير التغيير التجريبي", OrganizationEntityId = 1, IsActive = true, CreatedDate = new DateTime(2026, 1, 1) }
            );

            // 4. ربط المستخدمين بالأدوار (UserRoles)
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId = 1, UserId = 1, RoleId = 3, AssignedDate = new DateTime(2026, 1, 1), IsActive = true }, // admin -> COMMITTEE_CHAIR
                new UserRole { UserRoleId = 2, UserId = 2, RoleId = 2, AssignedDate = new DateTime(2026, 1, 1), IsActive = true }  // ambassador -> AMBASSADOR
            );
        }
    }
}