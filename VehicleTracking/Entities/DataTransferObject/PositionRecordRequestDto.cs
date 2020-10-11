using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public class PositionRecordRequestDto
    {
        [Required]
        [Range(10, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int DiviseId { get; set; }

        [Required]
        [Range(1, 1000000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float Latitude { get; set; }

        [Required]
        [Range(1, 1000000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float Longtitude { get; set; }
    }
}
