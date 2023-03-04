using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public interface IComputerRepository
    {
        Task<IEnumerable<Computer>> GetAllComputersAsync(bool trackChanges);
        Task<Computer> GetComputerAsync(string computerId, bool trackChanges);
        void CreateComputer(Computer computer);
        void DeleteComputer(Computer computer);
    }
}
