namespace BT_Remunerare.TL.DTO
{
    public class VendorTotalSalesDTO
    {
        public VendorDTO? Vendor { get; set; }
        public decimal TotalSalesValue { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
