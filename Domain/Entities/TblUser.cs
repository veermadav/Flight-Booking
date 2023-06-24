using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TblUser
    {
        [Key]
        public string UserName { get; set; }
        public string? UsrPassword { get; set; }
        public string? UsrEmail { get; set; }
        public string? UsrPhoneNo { get; set; }
        public string? UsrRole { get; set; }
    }
}