namespace CTP.Domain.Enums
{
    public enum RecommendationStatus
    {
        RequiresCompletion = 1,     // تحتاج استكمال قبل الاعتماد
        AwaitingSupport = 2,        // بانتظار قرار الدعم
        ReadyForChair = 3,          // جاهزة لاعتماد رئيس اللجنة
        Approved = 4,               // معتمدة
        Rejected = 5,               // مرفوضة
        SupersededBySovereign = 6   // مستبدلة بتوصية سيادية
    }
}