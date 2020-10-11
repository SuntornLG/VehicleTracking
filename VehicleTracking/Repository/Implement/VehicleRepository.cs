
using Entities;
using Entities.Models;
using Repository.Interface;

namespace Repository.Implement
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    }
}
