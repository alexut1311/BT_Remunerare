using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Remunerare.TL.DTO
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public int NumberOfProducts { get; set; }
        public VendorDTO? SaleVendor { get; set; }

        public ProductDTO? SaleProduct { get; set; }

    }
}
