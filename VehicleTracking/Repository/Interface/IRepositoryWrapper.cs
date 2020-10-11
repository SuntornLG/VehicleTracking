using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepositoryWrapper
    {
        IVehicleRepository Vehicle { get; }
        IPositionTransactionRepository PositionTransaction { get; }

        IUserRepository Users { get; }

        Task<int> SaveAsync();
    }
}
