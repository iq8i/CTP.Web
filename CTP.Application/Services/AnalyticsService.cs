using CTP.Application.Interfaces.Services;

namespace CTP.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public List<ParetoItem> BuildPareto(List<string> barriers, int thresholdPercent = 80)
        {
            if (barriers == null || barriers.Count == 0)
                return new List<ParetoItem>();

            var grouped = barriers
                .Where(b => !string.IsNullOrWhiteSpace(b))
                .GroupBy(b => b.Trim())
                .Select(g => new { Barrier = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            var total = grouped.Sum(x => x.Count);
            var result = new List<ParetoItem>();
            int cumulative = 0;

            foreach (var item in grouped)
            {
                var pct = (int)Math.Round((double)item.Count / total * 100);
                cumulative += pct;

                result.Add(new ParetoItem
                {
                    Barrier = item.Barrier,
                    Count = item.Count,
                    Percentage = pct,
                    CumulativePercentage = cumulative
                });

                if (cumulative >= thresholdPercent)
                    break;
            }

            return result;
        }

        public double CalculateAverage(IEnumerable<int> values)
        {
            if (values == null) return 0;
            var list = values.ToList();
            return list.Count == 0 ? 0 : Math.Round(list.Average(), 1);
        }
    }
}