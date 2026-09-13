namespace CTP.Domain.Constants
{
    public static class AppRolesHelper
    {
        /// <summary>
        /// يحوّل كود الدور (بالإنجليزية) إلى اسمه العربي الرسمي
        /// </summary>
        public static string GetArabicName(string? roleCode) => roleCode switch
        {
            AppRoles.Staff => "المنسوبون",
            AppRoles.Manager => "القادة والمدراء المباشرون",
            AppRoles.Ambassador => "سفير التغيير",
            AppRoles.UnitLeader => "قائد الوحدة",
            AppRoles.CommitteeMember => "عضو لجنة شركاء التغيير",
            AppRoles.CommitteeChair => "رئيس لجنة شركاء التغيير",
            AppRoles.ChiefOfStaff => "رئيس فريق عمل القائد",
            AppRoles.DeputyLeader => "نائب القائد",
            AppRoles.Leader => "معالي القائد",
            AppRoles.CorporateComms => "الاتصال المؤسسي",
            _ => "مستخدم"
        };

        /// <summary>
        /// يحوّل كود الدور إلى فئة Badge المناسبة (للعرض)
        /// </summary>
        public static string GetBadgeClass(string? roleCode) => roleCode switch
        {
            AppRoles.Leader => "bg-danger",
            AppRoles.DeputyLeader => "bg-warning text-dark",
            AppRoles.ChiefOfStaff => "bg-info text-dark",
            AppRoles.CommitteeChair => "bg-primary",
            AppRoles.CommitteeMember => "bg-primary-subtle text-primary",
            AppRoles.UnitLeader => "bg-success",
            AppRoles.Ambassador => "bg-warning",
            AppRoles.Manager => "bg-secondary",
            AppRoles.CorporateComms => "bg-info",
            _ => "bg-secondary"
        };
    }
}