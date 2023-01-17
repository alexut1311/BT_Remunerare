namespace BT_Remunerare.Models
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }
        public int PeriodId { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public int NumberOfProducts { get; set; }
        public VendorViewModel? SaleVendor { get; set; }

        public ProductViewModel? SaleProduct { get; set; }
        public PeriodViewModel? SalePeriod { get; set; }
    }
}
