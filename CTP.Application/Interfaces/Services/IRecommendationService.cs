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
        Task<IEnumerable<Recommendation>> GetPendingChairApprovalsAsync();
        Task<Recommendation?> GetRecommendationByReportIdAsync(int reportId);
        Task<IEnumerable<Recommendation>> GetApprovedRecommendationsAsync();
        Task<IEnumerable<Recommendation>> GetApprovedForInstitutionalReportAsync();
        Task<IEnumerable<Recommendation>> GetApprovedForImpactDashboardAsync();
        Task<IEnumerable<Recommendation>> GetApprovedForExecutiveSummaryAsync();
        Task<bool> ApproveForChairAsync(
            int reportId,
            bool includeInInstitutionalReport,
            bool includeInImpactDashboard,
            bool includeInExecutiveSummary,
            string? chairReviewNotes);
        Task<bool> RejectChairRecommendationAsync(int reportId, string? chairReviewNotes);
        Task<bool> UpdateRecommendationPlacementAsync(int reportId, string placement, bool enabled);
        /// <summary>
        /// B.4b: حفظ توصية كاملة مع كل الحقول الـ16
        /// </summary>
        Task<string> CreateFullRecommendationAsync(
            int reportId,
            string gapType,
            string evidence,
            string cause,
            string gapEffect,
            string suggestedAction,
            string actionOwner,
            string duration,
            string successIndicator,
            string impactMeasure,
            string expectedImpact,
            string supportDecision,
            string escalation,
            bool requiresSupport);
        Task<bool> UpdateRecommendationEscalationAsync(int recommendationId, string target);
    }
}