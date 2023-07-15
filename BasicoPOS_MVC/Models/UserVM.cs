using BasicPOS.Models;

namespace BasicoPOS_MVC.Models
{
    public class UserVM
    {
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public int? IdRole { get; set; }
        public string? NombreRole { get; set; } //nuevo
        public string? PhotoUrl { get; set; }
        public int? IsActive { get; set; } //cambio de bool a int
    }
}
