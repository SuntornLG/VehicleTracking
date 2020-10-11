
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class RoleMaster
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 15 characters")]
        public string RoleName { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Length must be less then 15 characters")]
        public string RoleCode { get; set; }
    }
}
