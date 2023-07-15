namespace BasicoPOS_MVC.Models
{
    public class BusinessVM
    {
        public int IdBusiness { get; set; }

        public string? ImgUrl { get; set; }

        public string? TaxNumber { get; set; }

        public string? BusinessName { get; set; }

        public string? BusinessEmail { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public decimal? Taxes { get; set; }

        public string? Currency { get; set; }
    }
}
