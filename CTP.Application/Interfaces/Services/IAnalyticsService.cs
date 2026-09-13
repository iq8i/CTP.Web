using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IAnalyticsService
    {
        List<ParetoItem> BuildPareto(List<string> barriers, int thresholdPercent = 80);
        double CalculateAverage(IEnumerable<int> values);
    }

    public class ParetoItem
    {
        public string Barrier { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Percentage { get; set; }
        public int CumulativePercentage { get; set; }
    }
}