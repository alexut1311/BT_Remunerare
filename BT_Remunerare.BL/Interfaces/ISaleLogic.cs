using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface ISaleLogic
    {
        Response AddSale(SaleDTO saleDTO);
        SaleDTO? GetSaleById(int saleId);
        Response UpdateSale(SaleDTO saleDTO);
        Response DeleteSale(int saleId);
        IList<SaleDTO> GetAllSales();
        TotalSalesDTO GetTotalSalesValueByPeriodId(int periodId);
    }
}
