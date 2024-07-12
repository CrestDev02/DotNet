using Microsoft.EntityFrameworkCore;
using UserManagement.Database.Entity.DataAccess.DTOs;

namespace UserManagement.Database.Entity.DataAccess.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //Tables
        public virtual DbSet<UserResultModel> users { get; set; }
    }
}
