namespace BT_Remunerare.TL.DTO
{
    public class PeriodDTO
    {
        public int PeriodId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public virtual ICollection<SaleDTO>? Sales { get; set; }

        public virtual ICollection<SalesRemunerationRuleDTO>? SalesRemunerationRules { get; set; }
    }
}
