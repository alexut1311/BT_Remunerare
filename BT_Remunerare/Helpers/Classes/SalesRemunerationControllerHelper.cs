using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.Helpers.Classes
{
    public class SalesRemunerationControllerHelper : ISalesRemunerationRuleControllerHelper<SalesRemunerationRuleViewModel, SalesRemunerationRuleDTO>
    {
        public SalesRemunerationRuleDTO BuildDTO(SalesRemunerationRuleViewModel salesRemunerationRuleViewModel)
        {
            return new SalesRemunerationRuleDTO
            {
                RemunerationId = salesRemunerationRuleViewModel.RemunerationId,
                PeriodId = salesRemunerationRuleViewModel.PeriodId,
                ProductId = salesRemunerationRuleViewModel.ProductId,
                Remuneration = salesRemunerationRuleViewModel.Remuneration
            };
        }

        public IList<SalesRemunerationRuleViewModel> BuildListViewModel(IList<SalesRemunerationRuleDTO> salesRemunerationRuleDTOs)
        {
            return salesRemunerationRuleDTOs.Select(salesRemunerationRuleDTO => new SalesRemunerationRuleViewModel
            {
                RemunerationId = salesRemunerationRuleDTO.RemunerationId,
                PeriodId = salesRemunerationRuleDTO.PeriodId,
                ProductId = salesRemunerationRuleDTO.ProductId,
                Remuneration = salesRemunerationRuleDTO.Remuneration
            }).ToList();
        }

        public IList<SalesRemunerationRuleViewModel> BuildListViewModelWithProductAndPeriod(IList<SalesRemunerationRuleDTO> salesRemunerationRuleDTOs)
        {
            return salesRemunerationRuleDTOs.Select(salesRemunerationRuleDTO => new SalesRemunerationRuleViewModel
            {
                RemunerationId = salesRemunerationRuleDTO.RemunerationId,
                PeriodId = salesRemunerationRuleDTO.PeriodId,
                ProductId = salesRemunerationRuleDTO.ProductId,
                Remuneration = salesRemunerationRuleDTO.Remuneration,
                SalesRemunerationProduct = salesRemunerationRuleDTO.SalesRemunerationProduct != null ? new ProductViewModel
                {
                    ProductId = salesRemunerationRuleDTO.SalesRemunerationProduct.ProductId,
                    ProductName = salesRemunerationRuleDTO.SalesRemunerationProduct.ProductName,
                } : null,
                SalesRemunerationPeriod = salesRemunerationRuleDTO.SalesRemunerationPeriod != null ? new PeriodViewModel
                {
                    PeriodId = salesRemunerationRuleDTO.SalesRemunerationPeriod.PeriodId,
                    Year = salesRemunerationRuleDTO.SalesRemunerationPeriod.Year,
                    Month = salesRemunerationRuleDTO.SalesRemunerationPeriod.Month,
                } : null,
            }).ToList();
        }

        public SalesRemunerationRuleViewModel BuildViewModel(SalesRemunerationRuleDTO? dTO)
        {
            throw new NotImplementedException();
        }
    }
}
