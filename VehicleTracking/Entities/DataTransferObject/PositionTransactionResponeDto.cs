
using System.Collections.Generic;

namespace Entities.DataTransferObject
{

    public class PositionTransactionResponeDto
    {
        public int DiviseId { get; set; }
        public string LicensePlateNumber { get; set; }
        public string VehicleName { get; set; }

        public List<Position> Positions { get; set; }
    }
}
