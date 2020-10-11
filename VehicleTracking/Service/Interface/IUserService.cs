
using Entities.DataTransferObject;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {

        Task<UserResponseDto> AuthenticateAsync(string username, string password);
        Task<UserRegisterResponseDto> CreateAsync(UserRegisterRequestDto user, string password);

    }
}
