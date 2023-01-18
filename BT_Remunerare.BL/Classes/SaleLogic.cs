using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Classes
{
    public class SaleLogic : ISaleLogic
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISalesRemunerationRepository _salesRemunerationRepository;

        public SaleLogic(ISaleRepository saleRepository, ISalesRemunerationRepository salesRemunerationRepository)
        {
            _saleRepository = saleRepository;
            _salesRemunerationRepository = salesRemunerationRepository;
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

        public TotalSalesDTO GetTotalSalesValueByPeriodId(int periodId)
        {
            IList<SaleDTO> totalSalesDTO = _saleRepository.GetSalesByPeriodId(periodId);
            TotalSalesDTO totalSalesValueDTO = new TotalSalesDTO();
            foreach (SaleDTO saleDTO in totalSalesDTO)
            {
                SalesRemunerationRuleDTO salesRemunerationRuleDTO = _salesRemunerationRepository.GetSalesRemunerationByPeriodAndProductId(saleDTO.PeriodId, saleDTO.ProductId);
                if (salesRemunerationRuleDTO != null)
                {
                    VendorTotalSalesDTO vendorTotalSales = new()
                    { Vendor = saleDTO.SaleVendor,Product=saleDTO.SaleProduct, TotalSalesValue = saleDTO.NumberOfProducts * salesRemunerationRuleDTO.Remuneration };
                    if (totalSalesValueDTO.TotalSales.ContainsKey(saleDTO.ProductId))
                    {
                        totalSalesValueDTO.TotalSales[saleDTO.ProductId].Add(vendorTotalSales);
                    }
                    else
                    {
                        totalSalesValueDTO.TotalSales.Add(saleDTO.ProductId, new List<VendorTotalSalesDTO> { vendorTotalSales });
                    }
                }
            }

            return totalSalesValueDTO;
        }
    }
}
