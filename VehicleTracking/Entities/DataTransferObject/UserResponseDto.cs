

namespace Entities.DataTransferObject
{
    public class UserResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string RoleCode { get; set; }
        public string Token { get; set; }
    }
}
