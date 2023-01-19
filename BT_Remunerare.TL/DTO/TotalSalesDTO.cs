namespace BT_Remunerare.TL.DTO
{
    public class TotalSalesDTO
    {
        public IDictionary<int, IList<VendorTotalSalesDTO>>? TotalSales { get; set; }
        public TotalSalesDTO()
        {
            TotalSales = new Dictionary<int, IList<VendorTotalSalesDTO>>();
        }
    }
}
