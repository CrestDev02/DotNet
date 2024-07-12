using System.Data;
using System.Data.SqlClient;
using UserManagement.Database.Entity.Common;
using UserManagement.Database.Entity.DataAccess.Context;
using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.DataAccess.Models;
using UserManagement.Database.Entity.Generic;
using UserManagement.Database.Entity.IRepositories;

namespace UserManagement.Database.Entity.Repositories
{
    public class UserRepository : GenericRepository<UserResultModel>, IUserRepository
    {
        private readonly IADORepository _linqADORepository;
        private readonly IExceptionRepository _applicationExceptionRepository;

        public UserRepository(ApplicationContext context, IADORepository linqADORepository, IExceptionRepository applicationExceptionRepository) : base(context)
        {
            _linqADORepository = linqADORepository;
            _applicationExceptionRepository = applicationExceptionRepository;
        }

        public async Task<IEnumerable<UserResultModel>> GetUsers()
        {
            try
            {
                return GetAll().Where(x => x.IsDeleted == false).Select(x => new UserResultModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    EmailConfirmed = x.EmailConfirmed,
                    Password = x.Password,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                    TwoFactorEnabled = x.TwoFactorEnabled,
                    Address = x.Address,
                    Zip = x.Zip,
                    CityId = x.CityId,
                    StateId = x.StateId,
                    CountryId = x.CountryId,
                    IsActive = x.IsActive,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    LastUpdated = x.LastUpdated,
                    LastUpdatedBy = x.LastUpdatedBy,
                    IsDeleted = x.IsDeleted
                }).AsEnumerable();
            }
            catch (Exception ex)
            {
                await _applicationExceptionRepository.AddAsync(ex.ConvertEx());
                return Enumerable.Empty<UserResultModel>();
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            try
            {
                var user = await GetAsync(id);
                if (user == null)
                    return new UserModel();
                else
                    return new UserModel()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        Address = user.Address,
                        Zip = user.Zip,
                        CityId = user.CityId,
                        StateId = user.StateId,
                        CountryId = user.CountryId,
                        IsActive = user.IsActive,
                        CreatedOn = user.CreatedOn,
                        CreatedBy = user.CreatedBy,
                        LastUpdated = user.LastUpdated,
                        LastUpdatedBy = user.LastUpdatedBy,
                        IsDeleted = user.IsDeleted
                    };
            }
            catch (Exception ex)
            {
                await _applicationExceptionRepository.AddAsync(ex.ConvertEx());
                return new UserModel();
            }
        }

        public async Task<ResultModel> SaveUser(UserModel model)
        {
            try
            {
                IReadOnlyList<SqlParameter> sqlParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = model.Id },
                    new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.NVarChar, Value = model.UserName },
                    new SqlParameter() { ParameterName = "@Email", SqlDbType = SqlDbType.NVarChar, Value = model.Email },
                    new SqlParameter() { ParameterName = "@Password", SqlDbType = SqlDbType.NVarChar, Value = model.Password },
                    new SqlParameter() { ParameterName = "@FirstName", SqlDbType = SqlDbType.NVarChar, Value = model.FirstName },
                    new SqlParameter() { ParameterName = "@LastName", SqlDbType = SqlDbType.NVarChar, Value = model.LastName },
                    new SqlParameter() { ParameterName = "@PhoneNumber", SqlDbType = SqlDbType.NVarChar, Value = model.PhoneNumber },
                    new SqlParameter() { ParameterName = "@LoginUserId", SqlDbType = SqlDbType.Int, Value = 1 }
                };

                var result = await _linqADORepository.ExecuteDataTableAsnyc("AddEditUserDetails", sqlParams.ToArray());
                return new ResultModel()
                {
                    Error = Convert.ToBoolean(result.Rows[0][0]),
                    Message = Convert.ToString(result.Rows[0][1])
                };
            }
            catch (Exception ex)
            {
                await _applicationExceptionRepository.AddAsync(ex.ConvertEx());
                return new ResultModel()
                {
                    Error = true,
                    Message = Convert.ToString(ex.Message)
                };
            }
        }

        public async Task<ResultModel> DeleteUser(int id)
        {
            try
            {
                IReadOnlyList<SqlParameter> sqlParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }
                };

                var result = await _linqADORepository.ExecuteDataTableAsnyc("DeleteUser", sqlParams.ToArray());
                return new ResultModel()
                {
                    Error = Convert.ToBoolean(result.Rows[0][0]),
                    Message = Convert.ToString(result.Rows[0][1])
                };
            }
            catch (Exception ex)
            {
                await _applicationExceptionRepository.AddAsync(ex.ConvertEx());
                return new ResultModel()
                {
                    Error = true,
                    Message = Convert.ToString(ex.Message)
                };
            }
        }
    }
}
