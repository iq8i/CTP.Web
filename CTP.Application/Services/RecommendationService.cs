using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationService(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        // أضف هذه الدالة داخل الكلاس إذا لم تكن موجودة
        public async Task<IEnumerable<Recommendation>> GetCommitteeRecommendationsAsync()
        {
            return await _recommendationRepository.GetAllWithDetailsAsync();
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
            string status = requiresSupport ? "بانتظار قرار الدعم" : "جاهزة لاعتماد رئيس اللجنة";
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