using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models
{
    public class LoginModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter email address"), MaxLength(45)]
        public string? EmailAddress { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter password"), MaxLength(100)]
        public string? Password { get; set; }
    }
}
