using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Repository.Extensions;

namespace PowerConsumptionAPI.Repository
{
    public class ComputerRepository : RepositoryBase<Computer>, IComputerRepository
    {
        public ComputerRepository(RepositoryContext _repositoryContext) 
            : base(_repositoryContext)
        {

        }

        public void CreateComputer(Computer computer) => Create(computer);

        public void DeleteComputer(Computer computer) => Delete(computer);

        public async Task<IEnumerable<Computer>> GetAllComputersAsync(ComputerParameters param, bool trackChanges) =>
            await FindAll(trackChanges)
            .Sort(param.OrderBy)
            .Skip(param.PrevCount)
            .Take(param.Count)
            .ToListAsync();

        public async Task<Computer> GetComputerAsync(string computerId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(computerId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
