using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IComputerRepository _computerRepository;
        private IPowerConsumptionRepository _powerConsumptionRepository;
        private ILimitRepository _limitRepository;
        private IElectricityCostRepository _electricityCostRepository;

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

        public ILimitRepository Limit
        {
            get
            {
                if (_limitRepository == null)
                    _limitRepository = new LimitRepository(_repositoryContext);

                return _limitRepository;
            }
        }   

        public IElectricityCostRepository ElectricityCost
        {
            get
            {
                if (_electricityCostRepository == null)
                    _electricityCostRepository = new ElectricityCostRepository(_repositoryContext);

                return _electricityCostRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
        public void Save() => _repositoryContext.SaveChanges();
    }
}
