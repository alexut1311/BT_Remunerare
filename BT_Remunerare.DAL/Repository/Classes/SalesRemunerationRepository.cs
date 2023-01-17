using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
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

        public Response AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationRuleDTO)
        {
            try
            {
                if (_applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == salesRemunerationRuleDTO.PeriodId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No period with period id {salesRemunerationRuleDTO.PeriodId} was found" };
                }

                if (_applicationDBContext.Products.FirstOrDefault(x => x.ProductId == salesRemunerationRuleDTO.ProductId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No product with product id {salesRemunerationRuleDTO.ProductId} was found" };
                }

                SalesRemunerationRule newSalesRemunerationRule = new()
                {
                    PeriodId = salesRemunerationRuleDTO.PeriodId,
                    ProductId = salesRemunerationRuleDTO.ProductId,
                    Remuneration = salesRemunerationRuleDTO.Remuneration,
                };
                _ = _applicationDBContext.SalesRemunerationRules.Add(newSalesRemunerationRule);
                _ = _applicationDBContext.SaveChanges();
                return new Response { IsSuccesful = true };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public Response DeleteSalesRemuneration(int salesRemunerationId)
        {
            try
            {
                SalesRemunerationRule salesRemunerationById = _applicationDBContext.SalesRemunerationRules.FirstOrDefault(x => x.RemunerationId == salesRemunerationId);

                if (salesRemunerationById != null)

                {
                    _ = _applicationDBContext.SalesRemunerationRules.Remove(salesRemunerationById);
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No sales remuneration with sales remuneration id {salesRemunerationId} was found" };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules()
        {
            IList<SalesRemunerationRuleDTO> salesRemunerationRuleDTOs = _applicationDBContext.SalesRemunerationRules.Include(x => x.RemunerationProduct).Include(x => x.SalesRemunerationPeriod).Select(salesRemunerationRule => new SalesRemunerationRuleDTO
            {
                RemunerationId = salesRemunerationRule.RemunerationId,
                PeriodId = salesRemunerationRule.PeriodId,
                ProductId = salesRemunerationRule.ProductId,
                Remuneration = salesRemunerationRule.Remuneration,
                SalesRemunerationProduct = new ProductDTO { ProductId = salesRemunerationRule.RemunerationProduct.ProductId, ProductName = salesRemunerationRule.RemunerationProduct.ProductName },
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
                                RemunerationId = salesRemunerationById.RemunerationId,
                                PeriodId = salesRemunerationById.PeriodId,
                                ProductId = salesRemunerationById.ProductId,
                                Remuneration = salesRemunerationById.Remuneration,
                                SalesRemunerationProduct = new ProductDTO { ProductId = salesRemunerationById.RemunerationProduct.ProductId, ProductName = salesRemunerationById.RemunerationProduct.ProductName },
                                SalesRemunerationPeriod = new PeriodDTO { PeriodId = salesRemunerationById.SalesRemunerationPeriod.PeriodId, Year = salesRemunerationById.SalesRemunerationPeriod.Year, Month = salesRemunerationById.SalesRemunerationPeriod.Month },
                            };
        }

        public Response UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationRuleDTO)
        {
            try
            {
                if (_applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == salesRemunerationRuleDTO.PeriodId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No period with period id {salesRemunerationRuleDTO.PeriodId} was found" };
                }

                if (_applicationDBContext.Products.FirstOrDefault(x => x.ProductId == salesRemunerationRuleDTO.ProductId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No product with product id {salesRemunerationRuleDTO.ProductId} was found" };
                }

                SalesRemunerationRule salesRemunerationById = _applicationDBContext.SalesRemunerationRules.FirstOrDefault(x => x.RemunerationId == salesRemunerationRuleDTO.RemunerationId);

                if (salesRemunerationById != null)
                {
                    salesRemunerationById.PeriodId = (salesRemunerationById.PeriodId == salesRemunerationRuleDTO.PeriodId) || salesRemunerationRuleDTO.PeriodId == 0 ? salesRemunerationById.PeriodId : salesRemunerationRuleDTO.PeriodId;
                    salesRemunerationById.ProductId = (salesRemunerationById.ProductId == salesRemunerationRuleDTO.ProductId) || salesRemunerationRuleDTO.ProductId == 0 ? salesRemunerationById.ProductId : salesRemunerationRuleDTO.ProductId;
                    salesRemunerationById.Remuneration = (salesRemunerationById.Remuneration == salesRemunerationRuleDTO.Remuneration) || salesRemunerationRuleDTO.Remuneration != 0 ? salesRemunerationById.Remuneration : salesRemunerationRuleDTO.Remuneration;
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No sales remuneration with sales remuneration id {salesRemunerationRuleDTO.RemunerationId} was found" };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }
    }
}
