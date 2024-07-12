using Microsoft.AspNetCore.Authentication;
using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.DataAccess.Models;
using UserManagement.Database.Entity.IRepositories;
using UserManagement.Web.BusinessLogic.Interfaces;

namespace UserManagement.Web.BusinessLogic.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<IEnumerable<UserResultModel>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserModel> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        public Task<ResultModel> Save(UserModel model)
        {
            return _userRepository.SaveUser(model);
        }

        public Task<ResultModel> Delete(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}
