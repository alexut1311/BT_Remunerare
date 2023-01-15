using System.ComponentModel.DataAnnotations;

namespace BT_Remunerare.DAL.Entities
{
    public class Period
    {
        [Key]
        public int PeriodId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        public virtual ICollection<Sale>? Sales { get; set; }

        public virtual ICollection<SalesRemunerationRule>? SalesRemunerationRules { get; set; }

    }
}
