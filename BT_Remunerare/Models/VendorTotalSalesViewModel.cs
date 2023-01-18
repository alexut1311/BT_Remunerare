namespace BT_Remunerare.Models
{
    public class VendorTotalSalesViewModel
    {
        public VendorViewModel? Vendor { get; set; }
        public decimal TotalSalesValue { get; set; }
        public ProductViewModel? Product { get; set; }
    }
}
