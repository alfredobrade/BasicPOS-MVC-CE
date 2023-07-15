
namespace BasicoPOS_MVC.Models
{
    public class MenuVM
    {
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Controller { get; set; }
        public string? ActionPage { get; set; }
        public virtual ICollection<MenuVM> SubMenus { get; set; }
    }
}
