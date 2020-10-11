
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public class UserRegisterRequestDto 
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Length must be less then 30 characters")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value more than 0")]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Length must be less then 30 characters"),
        MinLength(8, ErrorMessage = "Length must be more then 7 characters")]
        public string Password { get; set; }
    }
}
