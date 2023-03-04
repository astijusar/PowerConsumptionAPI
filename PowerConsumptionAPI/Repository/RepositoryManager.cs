using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IComputerRepository _computerRepository;
        private IPowerConsumptionRepository _powerConsumptionRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IComputerRepository Computer
        {
            get
            {
                if (_computerRepository == null)
                    _computerRepository = new ComputerRepository(_repositoryContext);

                return _computerRepository;
            }
        }

        public IPowerConsumptionRepository PowerConsumption
        {
            get
            {
                if (_powerConsumptionRepository == null)
                        _powerConsumptionRepository = new PowerConsumptionRepository(_repositoryContext);

                return _powerConsumptionRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
