using System.ComponentModel.DataAnnotations;

namespace UserManagement.Database.Entity.DataAccess.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter username"), MaxLength(45)]
        public string? UserName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter email address"), MaxLength(45)]
        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter password"), MaxLength(30)]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        public string? Password { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter confirm password"), MaxLength(30)]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "Confirm password does not match with password")]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter first name"), MaxLength(45)]
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter last name"), MaxLength(45)]
        public string? LastName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter phone number"), MaxLength(10)]
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
