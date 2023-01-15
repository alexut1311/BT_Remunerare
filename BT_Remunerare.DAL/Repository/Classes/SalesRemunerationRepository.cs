using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.DTO;
using Microsoft.EntityFrameworkCore;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class SalesRemunerationRepository : ISalesRemunerationRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public SalesRemunerationRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationRuleDTO)
        {
            SalesRemunerationRule newSalesRemunerationRule = new()
            {
                PeriodId = salesRemunerationRuleDTO.PeriodId,
                ProductId = salesRemunerationRuleDTO.ProductId,
                Remuneration = salesRemunerationRuleDTO.Remuneration,
            };
            _ = _applicationDBContext.SalesRemunerationRules.Add(newSalesRemunerationRule);
            _ = _applicationDBContext.SaveChanges();
        }

        public void DeleteSalesRemuneration(int salesRemunerationId)
        {
            SalesRemunerationRule salesRemunerationById = _applicationDBContext.SalesRemunerationRules.FirstOrDefault(x => x.RemunerationId == salesRemunerationId);
            if (salesRemunerationById != null)
            {
                _ = _applicationDBContext.SalesRemunerationRules.Remove(salesRemunerationById);
                _ = _applicationDBContext.SaveChanges();
            }
        }

        public IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules()
        {
            IList<SalesRemunerationRuleDTO> salesRemunerationRuleDTOs = _applicationDBContext.SalesRemunerationRules.Include(x => x.RemunerationProduct).Include(x => x.SalesRemunerationPeriod).Select(salesRemunerationRule => new SalesRemunerationRuleDTO
            {
                PeriodId = salesRemunerationRule.PeriodId,
                ProductId = salesRemunerationRule.ProductId,
                Remuneration = salesRemunerationRule.Remuneration,
                RemunerationProduct = new ProductDTO { ProductId = salesRemunerationRule.RemunerationProduct.ProductId, ProductName = salesRemunerationRule.RemunerationProduct.ProductName },
                SalesRemunerationPeriod = new PeriodDTO { PeriodId = salesRemunerationRule.SalesRemunerationPeriod.PeriodId, Year = salesRemunerationRule.SalesRemunerationPeriod.Year, Month = salesRemunerationRule.SalesRemunerationPeriod.Month },
            }).ToList();
            return salesRemunerationRuleDTOs;
        }

        public SalesRemunerationRuleDTO? GetSalesRemunerationById(int salesRemunerationId)
        {
            SalesRemunerationRule salesRemunerationById = _applicationDBContext.SalesRemunerationRules.Include(x => x.RemunerationProduct).Include(x => x.SalesRemunerationPeriod).FirstOrDefault(x => x.RemunerationId == salesRemunerationId);
            return salesRemunerationById == null
                            ? null
                            : new SalesRemunerationRuleDTO
                            {
                                PeriodId = salesRemunerationById.PeriodId,
                                ProductId = salesRemunerationById.ProductId,
                                Remuneration = salesRemunerationById.Remuneration,
                                RemunerationProduct = new ProductDTO { ProductId = salesRemunerationById.RemunerationProduct.ProductId, ProductName = salesRemunerationById.RemunerationProduct.ProductName },
                                SalesRemunerationPeriod = new PeriodDTO { PeriodId = salesRemunerationById.SalesRemunerationPeriod.PeriodId, Year = salesRemunerationById.SalesRemunerationPeriod.Year, Month = salesRemunerationById.SalesRemunerationPeriod.Month },
                            };
        }

        public void UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationRuleDTO)
        {
            SalesRemunerationRule salesRemunerationById = _applicationDBContext.SalesRemunerationRules.FirstOrDefault(x => x.RemunerationId == salesRemunerationRuleDTO.RemunerationId);
            if (salesRemunerationById != null)
            {
                salesRemunerationById.PeriodId = salesRemunerationRuleDTO.PeriodId;
                salesRemunerationById.ProductId = salesRemunerationRuleDTO.ProductId;
                salesRemunerationById.Remuneration = salesRemunerationRuleDTO.Remuneration;
                _ = _applicationDBContext.SaveChanges();
            };
        }
    }
}
