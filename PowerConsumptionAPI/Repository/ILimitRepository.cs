using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public interface ILimitRepository
    {
        IEnumerable<Limit> GetAllLimits(LimitType type, bool trackChanges);
        Limit GetLimitById(Guid id, LimitType type, bool trackChanges);
        void CreateLimit(Limit limit);
        void DeleteLimit(Limit limit);
    }
}
