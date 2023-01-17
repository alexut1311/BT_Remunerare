using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.EntityFrameworkCore;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public SaleRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public Response AddSale(SaleDTO saleDTO)
        {
            try
            {
                if (_applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == saleDTO.PeriodId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No period with period id {saleDTO.PeriodId} was found" };
                }

                if (_applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == saleDTO.VendorId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No vendor with vendor id {saleDTO.VendorId} was found" };
                }

                if (_applicationDBContext.Products.FirstOrDefault(x => x.ProductId == saleDTO.ProductId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No product with product id {saleDTO.ProductId} was found" };
                }

                Sale newSale = new()
                {
                    PeriodId = saleDTO.PeriodId,
                    VendorId = saleDTO.VendorId,
                    ProductId = saleDTO.ProductId,
                    NumberOfProducts = saleDTO.NumberOfProducts,
                };
                _ = _applicationDBContext.Sales.Add(newSale);
                _ = _applicationDBContext.SaveChanges();
                return new Response { IsSuccesful = true };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public Response DeleteSale(int saleId)
        {
            try
            {
                Sale saleById = _applicationDBContext.Sales.FirstOrDefault(x => x.SaleId == saleId);
                if (saleById != null)

                {
                    _ = _applicationDBContext.Sales.Remove(saleById);
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No sale with sale id {saleId} was found" };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public IList<SaleDTO> GetAllSales()
        {
            IList<SaleDTO> saleDTOs = _applicationDBContext.Sales.Include(x => x.SaleProduct).Include(x => x.SaleVendor).Include(x => x.SalePeriod).Select(sale => new SaleDTO
            {
                SaleId = sale.SaleId,
                PeriodId = sale.PeriodId,
                VendorId = sale.VendorId,
                ProductId = sale.ProductId,
                NumberOfProducts = sale.NumberOfProducts,
                SaleProduct = new ProductDTO { ProductId = sale.SaleProduct.ProductId, ProductName = sale.SaleProduct.ProductName },
                SaleVendor = new VendorDTO { VendorId = sale.SaleVendor.VendorId, VendorName = sale.SaleVendor.VendorName },
                SalePeriod = new PeriodDTO { PeriodId = sale.SalePeriod.PeriodId, Year = sale.SalePeriod.Year, Month = sale.SalePeriod.Month }
            }).ToList();
            return saleDTOs;
        }

        public SaleDTO? GetSaleById(int saleId)
        {
            Sale saleById = _applicationDBContext.Sales.Include(x => x.SaleProduct).Include(x => x.SaleVendor).Include(x => x.SalePeriod).FirstOrDefault(x => x.SaleId == saleId);
            return saleById == null
                            ? null
                            : new SaleDTO
                            {
                                SaleId = saleById.SaleId,
                                PeriodId = saleById.PeriodId,
                                VendorId = saleById.VendorId,
                                ProductId = saleById.ProductId,
                                NumberOfProducts = saleById.NumberOfProducts,
                                SaleProduct = new ProductDTO { ProductId = saleById.SaleProduct.ProductId, ProductName = saleById.SaleProduct.ProductName },
                                SaleVendor = new VendorDTO { VendorId = saleById.SaleVendor.VendorId, VendorName = saleById.SaleVendor.VendorName },
                                SalePeriod = new PeriodDTO { PeriodId = saleById.SalePeriod.PeriodId, Year = saleById.SalePeriod.Year, Month = saleById.SalePeriod.Month }
                            };
        }

        public Response UpdateSale(SaleDTO saleDTO)
        {
            try
            {
                if (_applicationDBContext.Periods.FirstOrDefault(x => x.PeriodId == saleDTO.PeriodId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No period with period id {saleDTO.PeriodId} was found" };
                }

                if (_applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == saleDTO.VendorId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No vendor with vendor id {saleDTO.VendorId} was found" };
                }

                if (_applicationDBContext.Products.FirstOrDefault(x => x.ProductId == saleDTO.ProductId) == null)
                {
                    return new Response { IsSuccesful = false, ErrorMessage = $"No product with product id {saleDTO.ProductId} was found" };
                }

                Sale saleById = _applicationDBContext.Sales.FirstOrDefault(x => x.SaleId == saleDTO.SaleId);
                if (saleById != null)

                {
                    saleById.PeriodId = (saleById.PeriodId == saleDTO.PeriodId) || saleDTO.PeriodId == 0 ? saleById.PeriodId : saleDTO.PeriodId;
                    saleById.VendorId = (saleById.VendorId == saleDTO.VendorId) || saleDTO.VendorId == 0 ? saleById.VendorId : saleDTO.VendorId;
                    saleById.ProductId = (saleById.ProductId == saleDTO.ProductId) || saleDTO.ProductId == 0 ? saleById.ProductId : saleDTO.ProductId;
                    saleById.NumberOfProducts = (saleById.NumberOfProducts == saleDTO.NumberOfProducts) || saleDTO.NumberOfProducts == 0 ? saleById.NumberOfProducts : saleDTO.NumberOfProducts;
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No sale with sale id {saleDTO.PeriodId} was found" };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }
    }
}
