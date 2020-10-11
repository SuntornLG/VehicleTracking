

using Entities.Abstract;

namespace Entities.DataTransferObject
{
    public class VehicleRegisterResponseDto : VehicleBaseResponse
    {
        public override int DiviseId { get; set; }
        public override string LicensePlateNumber { get; set; }
        public override string VehicleName { get; set; }
    }
}
