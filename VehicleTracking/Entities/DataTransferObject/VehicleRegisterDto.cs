
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public class VehicleRegisterDto
    {
        [Required]
        [Range(10, 1000000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int DiviseId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string LicensePlateNumber { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string VehicleName { get; set; }

    }
}
