namespace ProductApp.Models
{
    public class ProductResponse
    {
        public string Currency { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new();
    }
}