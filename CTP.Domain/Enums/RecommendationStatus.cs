namespace CTP.Domain.Enums
{
    public enum RecommendationStatus
    {
        RequiresCompletion = 1,      // تحتاج استكمال
        AwaitingSupport = 2,          // بانتظار قرار الدعم
        ReadyForChair = 3,            // جاهزة لاعتماد رئيس اللجنة
        Approved = 4,                 // معتمدة (عند رئيس الفريق)
        Rejected = 5,                 // مرفوضة
        SupersededBySovereign = 6,    // مستبدلة
        EscalatedToDeputy = 7,        // مصعدة للنائب
        EscalatedToLeader = 8,        // مصعدة للقائد
        ClosedByChief = 9             // مُغلقة عند رئيس الفريق
    }
}