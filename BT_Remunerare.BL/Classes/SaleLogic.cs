using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Classes
{
    public class SaleLogic : ISaleLogic
    {
        private readonly ISaleRepository _saleRepository;

        public SaleLogic(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public void AddSale(SaleDTO saleDTO)
        {
            _saleRepository.AddSale(saleDTO);
        }

        public void DeleteSale(int saleId)
        {
            if (saleId == 0)
            {
                return;
            }
            _saleRepository.DeleteSale(saleId);
        }

        public IList<SaleDTO> GetAllSales()
        {
            return _saleRepository.GetAllSales();
        }

        public SaleDTO? GetSaleById(int saleId)
        {
            return saleId == 0 ? null : _saleRepository.GetSaleById(saleId);
        }

        public void UpdateSale(SaleDTO saleDTO)
        {
            _saleRepository.UpdateSale(saleDTO);
        }
    }
}
