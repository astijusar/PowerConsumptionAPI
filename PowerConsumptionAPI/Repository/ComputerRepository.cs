using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;

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

        public async Task<IEnumerable<Computer>> GetAllComputersAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .ToListAsync();

        public async Task<Computer> GetComputerAsync(string computerId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(computerId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
