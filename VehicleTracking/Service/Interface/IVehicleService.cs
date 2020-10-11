using Entities.DataTransferObject;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IVehicleService
    {
        Task<VehicleRegisterResponseDto> CreateAsync(VehicleRegisterDto vehicle);
    }
}
