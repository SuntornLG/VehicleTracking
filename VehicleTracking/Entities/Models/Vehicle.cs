using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Vehicle
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Range(10, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int DiviseId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string LicensePlateNumber { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string VehicleName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }

    }
}
