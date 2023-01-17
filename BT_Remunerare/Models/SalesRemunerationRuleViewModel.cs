namespace BT_Remunerare.Models
{
    public class SalesRemunerationRuleViewModel
    {
        public int RemunerationId { get; set; }
        public int PeriodId { get; set; }
        public int ProductId { get; set; }
        public int Remuneration { get; set; }
        public ProductViewModel? SalesRemunerationProduct { get; set; }
        public PeriodViewModel? SalesRemunerationPeriod { get; set; }
    }
}
