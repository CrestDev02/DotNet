using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.DataAccess.Models;
using UserManagement.Database.Entity.Generic;

namespace UserManagement.Database.Entity.IRepositories
{
    public interface IUserRepository : IGenericRepository<UserResultModel>
    {
        Task<IEnumerable<UserResultModel>> GetUsers();

        Task<UserModel> GetUser(int id);

        Task<ResultModel> SaveUser(UserModel model);

        Task<ResultModel> DeleteUser(int id);
    }
}
