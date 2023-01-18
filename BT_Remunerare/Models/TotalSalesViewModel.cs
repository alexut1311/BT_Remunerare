namespace BT_Remunerare.Models
{
    public class TotalSalesViewModel
    {
        public IDictionary<int, IList<VendorTotalSalesViewModel>>? TotalSales { get; set; }
        public TotalSalesViewModel()
        {
            TotalSales = new Dictionary<int, IList<VendorTotalSalesViewModel>>();
        }
    }
}
