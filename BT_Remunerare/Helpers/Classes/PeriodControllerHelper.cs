using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.Helpers.Classes
{
    public class PeriodControllerHelper : IPeriodControllerHelper<PeriodViewModel, PeriodDTO>
    {
        public PeriodDTO BuildDTO(PeriodViewModel periodViewModel)
        {
            return new PeriodDTO
            {
                PeriodId = periodViewModel.PeriodId,
                Year = periodViewModel.Year,
                Month = periodViewModel.Month,
            };
        }

        public IList<PeriodViewModel> BuildListViewModel(IList<PeriodDTO> periodDTOs)
        {

            return periodDTOs.Select(periodDTO => new PeriodViewModel
            {
                PeriodId = periodDTO.PeriodId,
                Year = periodDTO.Year,
                Month = periodDTO.Month,
            }).ToList();
        }

        public IList<PeriodViewModel> BuildListViewModelWithSalesAndRemuneration(IList<PeriodDTO> periodDTOs)
        {
            return periodDTOs.Select(periodDTO => new PeriodViewModel
            {
                PeriodId = periodDTO.PeriodId,
                Year = periodDTO.Year,
                Month = periodDTO.Month,
                Sales = periodDTO.Sales?.Select(saleDTO => new SaleViewModel
                {
                    SaleId = saleDTO.SaleId,
                    PeriodId = saleDTO.PeriodId,
                    VendorId = saleDTO.VendorId,
                    ProductId = saleDTO.ProductId,
                    NumberOfProducts = saleDTO.NumberOfProducts,
                }).ToList(),
                SalesRemunerationRules = periodDTO.SalesRemunerationRules?.Select(salesRemunerationRuleDTO => new SalesRemunerationRuleViewModel
                {
                    RemunerationId = salesRemunerationRuleDTO.RemunerationId,
                    PeriodId = salesRemunerationRuleDTO.PeriodId,
                    ProductId = salesRemunerationRuleDTO.ProductId,
                    Remuneration = salesRemunerationRuleDTO.Remuneration
                }).ToList(),
            }).ToList();
        }

        public PeriodViewModel BuildViewModel(PeriodDTO? periodDTO)
        {
            return new PeriodViewModel
            {
                PeriodId = periodDTO.PeriodId,
                Year = periodDTO.Year,
                Month = periodDTO.Month,
            };
        }
    }
}
