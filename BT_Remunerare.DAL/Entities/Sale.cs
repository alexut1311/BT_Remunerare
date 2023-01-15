using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_Remunerare.DAL.Entities
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        [ForeignKey("Period")]
        public int PeriodId { get; set; }
        [ForeignKey("Vendor")]
        public int VendorId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int NumberOfProducts { get; set; }
        public Vendor? SaleVendor { get; set; }

        public Product? SaleProduct { get; set; }
        public Period? SalePeriod { get; set; }

    }
}
