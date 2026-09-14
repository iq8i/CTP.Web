using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;
using CTP.Domain.Enums;

namespace CTP.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationService(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        public async Task<IEnumerable<Recommendation>> GetCommitteeRecommendationsAsync()
        {
            return await _recommendationRepository.GetAllWithDetailsAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetPendingChairApprovalsAsync()
        {
            return await _recommendationRepository.GetPendingChairApprovalsAsync();
        }

        public async Task<Recommendation?> GetRecommendationByReportIdAsync(int reportId)
        {
            return await _recommendationRepository.GetByMonthlyReportIdAsync(reportId);
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedRecommendationsAsync()
        {
            return await _recommendationRepository.GetApprovedRecommendationsAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedForInstitutionalReportAsync()
        {
            return await _recommendationRepository.GetApprovedForInstitutionalReportAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedForImpactDashboardAsync()
        {
            return await _recommendationRepository.GetApprovedForImpactDashboardAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedForExecutiveSummaryAsync()
        {
            return await _recommendationRepository.GetApprovedForExecutiveSummaryAsync();
        }

        public async Task<string> CreateFullRecommendationAsync(
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
    bool requiresSupport)
        {
            // توليد رقم مرجعي
            string recNumber = $"R-{DateTime.Now:MMdd}-{new Random().Next(100, 999)}";

            // تحديد الحالة
            var status = requiresSupport
                ? CTP.Domain.Enums.RecommendationStatus.AwaitingSupport
                : CTP.Domain.Enums.RecommendationStatus.ReadyForChair;

            var recommendation = new Recommendation
            {
                MonthlyReportId = reportId,
                RecommendationNumber = recNumber,
                GapType = gapType.Length > 100 ? gapType.Substring(0, 100) : gapType,
                Evidence = evidence,
                Cause = cause,
                GapEffect = gapEffect,
                SuggestedAction = suggestedAction,
                ActionOwner = actionOwner,
                Duration = duration,
                SuccessIndicator = successIndicator,
                ImpactMeasure = impactMeasure,
                ExpectedImpact = expectedImpact,
                SupportDecision = supportDecision,
                Escalation = escalation,
                Status = status,
                CreatedDate = DateTime.Now
            };

            await _recommendationRepository.AddAsync(recommendation);
            await _recommendationRepository.SaveChangesAsync();

            return recNumber;
        }
        public async Task<bool> ApproveForChairAsync(
            int reportId,
            bool includeInInstitutionalReport,
            bool includeInImpactDashboard,
            bool includeInExecutiveSummary,
            string? chairReviewNotes)
        {
            var recommendation = await _recommendationRepository.GetByMonthlyReportIdAsync(reportId);
            if (recommendation == null)
            {
                return false;
            }

            recommendation.Status = CTP.Domain.Enums.RecommendationStatus.Approved;
            recommendation.IncludeInInstitutionalReport = includeInInstitutionalReport;
            recommendation.IncludeInImpactDashboard = includeInImpactDashboard;
            recommendation.IncludeInExecutiveSummary = includeInExecutiveSummary;
            recommendation.ChairReviewNotes = chairReviewNotes;
            recommendation.ReviewedDate = DateTime.Now;

            await _recommendationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectChairRecommendationAsync(int reportId, string? chairReviewNotes)
        {
            var recommendation = await _recommendationRepository.GetByMonthlyReportIdAsync(reportId);
            if (recommendation == null)
            {
                return false;
            }

            recommendation.Status = CTP.Domain.Enums.RecommendationStatus.Rejected;
            recommendation.ChairReviewNotes = chairReviewNotes;
            recommendation.ReviewedDate = DateTime.Now;
            recommendation.IncludeInInstitutionalReport = false;
            recommendation.IncludeInImpactDashboard = false;
            recommendation.IncludeInExecutiveSummary = false;

            await _recommendationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRecommendationPlacementAsync(int reportId, string placement, bool enabled)
        {
            return await _recommendationRepository.UpdatePlacementAsync(reportId, placement, enabled);
        }

        public async Task<string> ProcessAndSaveAnalysisAsync(
            int reportId,
            string whyItMatters,
            string suggestedAction,
            string owner,
            string expectedImpact,
            bool requiresSupport)
        {
            // 1. توليد الرقم المرجعي
            string recNumber = $"R-{DateTime.Now:MMdd}-{new Random().Next(100, 999)}";

            // 2. تطبيق قواعد جودة التوصية (Business Rules)
            var status = requiresSupport
    ? CTP.Domain.Enums.RecommendationStatus.AwaitingSupport
    : CTP.Domain.Enums.RecommendationStatus.ReadyForChair;
            string gapType = whyItMatters.Length > 90 ? whyItMatters.Substring(0, 90) : whyItMatters;

            // 3. بناء الكيان
            var recommendation = new Recommendation
            {
                MonthlyReportId = reportId,
                RecommendationNumber = recNumber,
                GapType = gapType,
                SuggestedAction = suggestedAction,
                ActionOwner = owner,
                ExpectedImpact = expectedImpact,
                Status = status,
                CreatedDate = DateTime.Now
            };

            // 4. الحفظ
            await _recommendationRepository.AddAsync(recommendation);
            await _recommendationRepository.SaveChangesAsync();

            return recNumber;
        }
    }
}