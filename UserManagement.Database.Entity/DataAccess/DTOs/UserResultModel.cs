using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Database.Entity.DataAccess.DTOs
{
    [Table("tblUsers")]
    public class UserResultModel
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string? Address { get; set; }

        public string? Zip { get; set; }

        public Nullable<int> CityId { get; set; }

        public Nullable<int> StateId { get; set; }

        public Nullable<int> CountryId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public Nullable<DateTime> LastUpdated { get; set; }

        public Nullable<int> LastUpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
