using BasicPOS.Models;

namespace BasicoPOS_MVC.Models
{
    public class CategoryVM
    {
        public int IdCategory { get; set; }
        public string? Description { get; set; }
        public int? IsActive { get; set; }
    }
}
