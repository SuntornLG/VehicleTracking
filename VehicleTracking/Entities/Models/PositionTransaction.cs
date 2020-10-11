using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PositionTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public float Latitude { get; set; }
        [Required(ErrorMessage = "Latitude is required")]
        public float Longtitude { get; set; }
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Devise number is required")]
        [ForeignKey(nameof(Vehicle))]
        public int DiviseId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
