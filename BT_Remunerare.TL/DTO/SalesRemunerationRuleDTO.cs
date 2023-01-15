namespace BT_Remunerare.TL.DTO
{
    public class SalesRemunerationRuleDTO
    {
        public int RemunerationId { get; set; }
        public int PeriodId { get; set; }
        public int ProductId { get; set; }
        public int Remuneration { get; set; }
        public ProductDTO? RemunerationProduct { get; set; }
        public PeriodDTO? SalesRemunerationPeriod { get; set; }


    }
}
