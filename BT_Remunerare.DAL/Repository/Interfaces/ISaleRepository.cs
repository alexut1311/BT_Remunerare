using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface ISaleRepository
    {
        void AddSale(SaleDTO saleDTO);
        SaleDTO? GetSaleById(int saleId);
        void UpdateSale(SaleDTO saleDTO);
        void DeleteSale(int saleId);
        IList<SaleDTO> GetAllSales();
    }
}
