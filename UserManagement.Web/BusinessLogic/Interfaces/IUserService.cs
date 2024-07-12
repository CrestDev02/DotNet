using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.DataAccess.Models;

namespace UserManagement.Web.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResultModel>> GetUsers();

        Task<UserModel> GetUser(int id);

        Task<ResultModel> Save(UserModel model);

        Task<ResultModel> Delete(int id);
    }
}
