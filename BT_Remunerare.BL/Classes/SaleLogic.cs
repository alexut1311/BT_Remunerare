using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
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

        public Response AddSale(SaleDTO saleDTO)
        {
            return _saleRepository.AddSale(saleDTO);
        }

        public Response DeleteSale(int saleId)
        {
            return _saleRepository.DeleteSale(saleId);
        }

        public IList<SaleDTO> GetAllSales()
        {
            return _saleRepository.GetAllSales();
        }

        public SaleDTO? GetSaleById(int saleId)
        {
            return saleId == 0 ? null : _saleRepository.GetSaleById(saleId);
        }

        public Response UpdateSale(SaleDTO saleDTO)
        {
            return _saleRepository.UpdateSale(saleDTO);
        }
    }
}
