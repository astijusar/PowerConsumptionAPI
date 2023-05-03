using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public interface ILimitRepository
    {
        IEnumerable<Limit> GetAllLimits(LimitType type, bool trackChanges);
        Limit GetLimitById(Guid id, bool trackChanges, LimitType? type = null);
        void CreateLimit(Limit limit);
        void DeleteLimit(Limit limit);
    }
}
