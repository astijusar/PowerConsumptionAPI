using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public class LimitRepository : RepositoryBase<Limit>, ILimitRepository
    {
        public LimitRepository(RepositoryContext _repositoryContext)
            :base(_repositoryContext)
        {

        }

        public void CreateLimit(Limit limit) => Create(limit);

        public void DeleteLimit(Limit limit) => Delete(limit);

        public IEnumerable<Limit> GetAllLimits(LimitType type, bool trackChanges) =>
            FindByCondition(l => l.LimitType == type, trackChanges)
            .ToList();

        public Limit GetLimitById(Guid id, bool trackChanges, LimitType? type = null)
        {
            if (type == null)
            {
                return FindByCondition(l => l.Id.Equals(id), trackChanges)
                    .SingleOrDefault();
            }

            return FindByCondition(l => l.Id.Equals(id) && l.LimitType == type, trackChanges)
                    .SingleOrDefault();
        }
    }
}
