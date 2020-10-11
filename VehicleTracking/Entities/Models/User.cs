
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Length must be less then 30 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Devise number is required")]
        [ForeignKey(nameof(RoleMaster))]
        public int RoleId { get; set; }
        public RoleMaster RoleMaster { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
