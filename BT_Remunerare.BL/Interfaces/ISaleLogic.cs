using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface ISaleLogic
    {
        void AddSale(SaleDTO saleDTO);
        SaleDTO? GetSaleById(int saleId);
        void UpdateSale(SaleDTO saleDTO);
        void DeleteSale(int saleId);
        IList<SaleDTO> GetAllSales();
    }
}
