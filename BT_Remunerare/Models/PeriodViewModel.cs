namespace BT_Remunerare.Models
{
    public class PeriodViewModel
    {
        public int PeriodId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public virtual ICollection<SaleViewModel>? Sales { get; set; }

        public virtual ICollection<SalesRemunerationRuleViewModel>? SalesRemunerationRules { get; set; }
    }
}
