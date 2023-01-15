using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_Remunerare.DAL.Entities
{
    public class SalesRemunerationRule
    {
        [Key]
        public int RemunerationId { get; set; }
        [ForeignKey("Period")]
        public int PeriodId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int Remuneration { get; set; }
        public Product? RemunerationProduct { get; set; }
        public Period? SalesRemunerationPeriod { get; set; }


    }
}
