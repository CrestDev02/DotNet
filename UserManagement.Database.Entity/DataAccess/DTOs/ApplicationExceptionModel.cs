using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Database.Entity.DataAccess.DTOs
{
    [Table("tblApplicationException")]
    public class ApplicationExceptionModel
    {
        public int Id { get; set; }

        public string? Message { get; set; }

        public string? Source { get; set; }

        public string? Method { get; set; }

        public string? StackTrace { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
