namespace PowerConsumptionAPI.Repository
{
    public interface IRepositoryManager
    {
        IComputerRepository Computer { get; }
        IPowerConsumptionRepository PowerConsumption { get; }
        ILimitRepository Limit { get; }
        Task SaveAsync();
        void Save();
    }
}
