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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // إعداد المفتاح المركب (Composite Key) لجدول الربط
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // ربط المستخدم بالأدوار
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // بذر البيانات الأساسية (الأدوار الـ 13)
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleCode = "LEADER", RoleName = "معالي القائد" },
                new Role { RoleId = 2, RoleCode = "AMBASSADOR", RoleName = "سفير التغيير" },
                new Role { RoleId = 3, RoleCode = "COMMITTEE_CHAIR", RoleName = "رئيس لجنة شركاء التغيير" },
                new Role { RoleId = 4, RoleCode = "STAFF", RoleName = "المنسوبون" }
                // يمكنك إكمال باقي الأدوار هنا...
            );

            // بذر مستخدم تجريبي
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = "123456", // فحصنا المطابقة المباشرة مؤقتاً في الـ Controller
                    FullName = "مدير النظام التجريبي",
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1) // تاريخ ثابت لتجنب مشاكل الـ Migrations
                }
            );

            // ربط المستخدم التجريبي بدور (رئيس لجنة شركاء التغيير) كمثال لتجربة الشاشة
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserRoleId = 1,
                    UserId = 1,
                    RoleId = 3, // 3 = COMMITTEE_CHAIR حسب الأدوار التي أضفناها
                    AssignedDate = new DateTime(2026, 1, 1),
                    IsActive = true
                }
            );
        }
    }
}