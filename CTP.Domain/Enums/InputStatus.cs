namespace CTP.Domain.Enums
{
    public enum InputStatus
    {
        Received = 1,           // مستلم
        Routed = 2,             // موجه للجهة المختصة
        SupportIssued = 3,      // قرار دعم صادر
        Published = 4,          // منشور كقصة/ممارسة
        Escalated = 5           // مصعد عبر المسار الرسمي
    }
}