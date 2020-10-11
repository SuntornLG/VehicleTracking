

using Entities.Abstract;
using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class VehicleResponseDto : VehicleBaseResponse
    {
        public ICollection<PositionTransactionResponeDto> PositionTransaction { get; set; }
        public override int DiviseId { get; set; }
        public override string LicensePlateNumber { get; set; }
        public override string VehicleName { get; set; }
    }
}
