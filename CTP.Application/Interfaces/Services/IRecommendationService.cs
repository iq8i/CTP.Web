using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IRecommendationService
    {
        Task<string> ProcessAndSaveAnalysisAsync(
            int reportId,
            string whyItMatters,
            string suggestedAction,
            string owner,
            string expectedImpact,
            bool requiresSupport);
        Task<IEnumerable<Recommendation>> GetCommitteeRecommendationsAsync();

    }
}