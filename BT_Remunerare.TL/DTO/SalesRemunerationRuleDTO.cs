namespace BT_Remunerare.TL.DTO
{
    public class SalesRemunerationRuleDTO
    {
        public int RemunerationId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ProductId { get; set; }
        public int Remuneration { get; set; }
        public ProductDTO? RemunerationProduct { get; set; }

    }
}
