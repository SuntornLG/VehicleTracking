using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using LoggerService;
using Repository.Interface;
using Service.Interface;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Microsoft.Extensions.Configuration;
using Service.ExceptionHandler;

namespace Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWraper;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;

        public UserService(IRepositoryWrapper repositoryWraper, IMapper mapper, ILoggerManager loggerManager,
            IConfiguration configuration, IPasswordService passwordService)
        {
            _repositoryWraper = repositoryWraper;
            _mapper = mapper;
            _loggerManager = loggerManager;
            _configuration = configuration;
            _passwordService = passwordService;
        }
        public async Task<UserResponseDto> AuthenticateAsync(string username, string password)
        {

            // Check if username exists
            var user = await _repositoryWraper.Users
                .FindByCondition(x => x.Email == username)
                .Include(i => i.RoleMaster).FirstOrDefaultAsync();

            if (user == null)
                return null;

            // verify password
            bool isValitPassword = _passwordService.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            if (!isValitPassword)
                return null;

            var userModel = _mapper.Map<UserResponseDto>(user);
            userModel.RoleCode = user.RoleMaster.RoleCode;

            userModel.Token = AuthenticationConfig.GenerateJSONWebToken(_configuration, userModel);
            return userModel;
        }

        public async Task<UserRegisterResponseDto> CreateAsync(UserRegisterRequestDto user, string password)
        {
            try
            {
                var userModel = _mapper.Map<User>(user);
                var existingUser = _repositoryWraper.Users.FindByCondition(x => x.Email == user.Email).FirstOrDefault();
                if (existingUser != null)
                    throw new ServiceCustomException("This Username is already taken");

                var hashPassword = _passwordService.CreatePasswordHash(password);
                userModel.PasswordSalt = hashPassword.Item1;
                userModel.PasswordHash = hashPassword.Item2;

                _repositoryWraper.Users.Create(userModel);
                await _repositoryWraper.SaveAsync();

                return new UserRegisterResponseDto() { Id = userModel.Id, FirstName = userModel.FirstName, LastName = userModel.LastName };

            }
            catch (ServiceCustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error in file name UserService.cs on function CreateAsync : {ex}");
                throw ex;
            }
        }
    }
}
