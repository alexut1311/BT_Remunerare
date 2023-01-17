using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.EntityFrameworkCore;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class PeriodRepository : IPeriodRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public PeriodRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public Response AddPeriod(PeriodDTO periodDTO)
        {
            try
            {
                Period newPeriod = new()
                {
                    Year = periodDTO.Year,
                    Month = periodDTO.Month,
                };
                _ = _applicationDBContext.Periods.Add(newPeriod);
                _ = _applicationDBContext.SaveChanges();
                return new Response { IsSuccesful = true };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public Response DeletePeriod(int periodId)
        {
            try
            {
                Period periodById = _applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == periodId);
                if (periodById != null)
                {
                    _ = _applicationDBContext.Periods.Remove(periodById);
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No period with period id {periodId} was found" };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public IList<PeriodDTO> GetAllPeriods()
        {
            IList<PeriodDTO> periodDTOs = _applicationDBContext.Periods.Select(period => new PeriodDTO
            {
                PeriodId = period.PeriodId,
                Year = period.Year,
                Month = period.Month,
            }).ToList();
            return periodDTOs;
        }

        public IList<PeriodDTO> GetAllPeriodsWithSalesAndRemuneration()
        {
            IList<PeriodDTO> periodDTOs = _applicationDBContext.Periods.Include(x => x.Sales).Include(x => x.SalesRemunerationRules).Select(period => new PeriodDTO
            {
                PeriodId = period.PeriodId,
                Year = period.Year,
                Month = period.Month,
                Sales = period.Sales.Select(sale => new SaleDTO
                {
                    PeriodId = sale.PeriodId,
                    VendorId = sale.VendorId,
                    ProductId = sale.ProductId,
                    NumberOfProducts = sale.NumberOfProducts,
                    SaleProduct = new ProductDTO { ProductId = sale.SaleProduct.ProductId, ProductName = sale.SaleProduct.ProductName },
                    SaleVendor = new VendorDTO { VendorId = sale.SaleVendor.VendorId, VendorName = sale.SaleVendor.VendorName },
                    SalePeriod = new PeriodDTO { PeriodId = sale.SalePeriod.PeriodId, Year = sale.SalePeriod.Year, Month = sale.SalePeriod.Month }
                }).ToList(),
                SalesRemunerationRules = period.SalesRemunerationRules.Select(saleRemunerationRule => new SalesRemunerationRuleDTO
                {
                    PeriodId = saleRemunerationRule.PeriodId,
                    ProductId = saleRemunerationRule.ProductId,
                    Remuneration = saleRemunerationRule.Remuneration,
                    SalesRemunerationProduct = new ProductDTO { ProductId = saleRemunerationRule.RemunerationProduct.ProductId, ProductName = saleRemunerationRule.RemunerationProduct.ProductName },
                    SalesRemunerationPeriod = new PeriodDTO { PeriodId = saleRemunerationRule.SalesRemunerationPeriod.PeriodId, Year = saleRemunerationRule.SalesRemunerationPeriod.Year, Month = saleRemunerationRule.SalesRemunerationPeriod.Month },
                }).ToList(),
            }).ToList();
            return periodDTOs;
        }

        public PeriodDTO? GetPeriodById(int periodId)
        {
            Period periodById = _applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == periodId);
            return periodById == null
                ? null
                : new PeriodDTO
                {
                    PeriodId = periodById.PeriodId,
                    Year = periodById.Year,
                    Month = periodById.Month,
                };
        }

        public Response UpdatePeriod(PeriodDTO periodDTO)
        {
            try
            {
                Period periodById = _applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == periodDTO.PeriodId);
                if (periodById != null)
                {
                    periodById.Year = (periodById.Year == periodDTO.Year) || periodDTO.Year == 0 ? periodById.Year : periodDTO.Year;
                    periodById.Month = (periodById.Month == periodDTO.Month) || periodDTO.Month == 0 ? periodById.Month : periodDTO.Month;
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No period with period id {periodDTO.PeriodId} was found" };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }
    }
}
