namespace BT_Remunerare.TL.DTO
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public int PeriodId { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public int NumberOfProducts { get; set; }
        public VendorDTO? SaleVendor { get; set; }

        public ProductDTO? SaleProduct { get; set; }
        public PeriodDTO? SalePeriod { get; set; }


    }
}
