using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface ISaleRepository
    {
        Response AddSale(SaleDTO saleDTO);
        SaleDTO? GetSaleById(int saleId);
        Response UpdateSale(SaleDTO saleDTO);
        Response DeleteSale(int saleId);
        IList<SaleDTO> GetAllSales();
    }
}
