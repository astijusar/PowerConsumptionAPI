namespace PowerConsumptionAPI.Repository
{
    public interface IRepositoryManager
    {
        IComputerRepository Computer { get; }
        IPowerConsumptionRepository PowerConsumption { get; }
        Task SaveAsync();
    }
}
