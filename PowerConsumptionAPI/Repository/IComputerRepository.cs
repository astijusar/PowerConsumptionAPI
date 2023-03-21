using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;

namespace PowerConsumptionAPI.Repository
{
    public interface IComputerRepository
    {
        Task<IEnumerable<Computer>> GetAllComputersAsync(ComputerParameters parameters, bool trackChanges);
        Task<Computer> GetComputerAsync(string computerId, bool trackChanges);
        void CreateComputer(Computer computer);
        void DeleteComputer(Computer computer);
    }
}
