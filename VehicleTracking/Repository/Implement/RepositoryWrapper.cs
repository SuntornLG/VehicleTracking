using Entities;
using Repository.Interface;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private readonly RepositoryContext _repositoryContext;
        private IVehicleRepository _vehiclaRepository;
        private IPositionTransactionRepository _positionRepository;
        private IUserRepository _userRepository;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IVehicleRepository Vehicle
        {
            get
            {
                if (_vehiclaRepository == null)
                {
                    _vehiclaRepository = new VehicleRepository(_repositoryContext);
                }
                return _vehiclaRepository;
            }
        }

        public IPositionTransactionRepository PositionTransaction
        {
            get
            {
                if (_positionRepository == null)
                {
                    _positionRepository = new PositionTransactionRepository(_repositoryContext);
                }
                return _positionRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_repositoryContext);
                }
                return _userRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _repositoryContext.SaveChangesAsync();
        }
    }
}
