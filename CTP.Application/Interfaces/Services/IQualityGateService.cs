using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IQualityGateService
    {
        QualityGateResult Check(Recommendation recommendation);
    }

    public class QualityGateResult
    {
        public List<QualityGateItem> Items { get; set; } = new();
        public int TotalItems => Items.Count;
        public int PassedItems => Items.Count(i => i.IsPassed);
        public int MissingItems => TotalItems - PassedItems;
        public int CompletenessPercent => TotalItems == 0 ? 0 : (int)Math.Round((double)PassedItems / TotalItems * 100);
        public bool IsReadyForApproval => MissingItems == 0;
    }

    public class QualityGateItem
    {
        public int Number { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsPassed { get; set; }
    }
}