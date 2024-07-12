using UserManagement.Database.Entity.DataAccess.Context;
using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.Generic;
using UserManagement.Database.Entity.IRepositories;

namespace UserManagement.Database.Entity.Repositories
{
    public class ExceptionRepository : GenericRepository<ApplicationExceptionModel>, IExceptionRepository
    {
        public ExceptionRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
