using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
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

        public void AddSale(SaleDTO saleDTO)
        {
            Sale newSale = new()
            {
                PeriodId = saleDTO.PeriodId,
                VendorId = saleDTO.VendorId,
                ProductId = saleDTO.ProductId,
                NumberOfProducts = saleDTO.NumberOfProducts,
            };
            _ = _applicationDBContext.Sales.Add(newSale);
            _ = _applicationDBContext.SaveChanges();
        }

        public void DeleteSale(int saleId)
        {
            Sale saleById = _applicationDBContext.Sales.FirstOrDefault(x => x.SaleId == saleId);
            if (saleById != null)
            {
                _ = _applicationDBContext.Sales.Remove(saleById);
                _ = _applicationDBContext.SaveChanges();
            }
        }

        public IList<SaleDTO> GetAllSales()
        {
            IList<SaleDTO> saleDTOs = _applicationDBContext.Sales.Include(x => x.SaleProduct).Include(x => x.SaleVendor).Include(x => x.SalePeriod).Select(sale => new SaleDTO
            {
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
                                PeriodId = saleById.PeriodId,
                                VendorId = saleById.VendorId,
                                ProductId = saleById.ProductId,
                                NumberOfProducts = saleById.NumberOfProducts,
                                SaleProduct = new ProductDTO { ProductId = saleById.SaleProduct.ProductId, ProductName = saleById.SaleProduct.ProductName },
                                SaleVendor = new VendorDTO { VendorId = saleById.SaleVendor.VendorId, VendorName = saleById.SaleVendor.VendorName },
                                SalePeriod = new PeriodDTO { PeriodId = saleById.SalePeriod.PeriodId, Year = saleById.SalePeriod.Year, Month = saleById.SalePeriod.Month }
                            };
        }

        public void UpdateSale(SaleDTO saleDTO)
        {
            Sale saleById = _applicationDBContext.Sales.FirstOrDefault(x => x.SaleId == saleDTO.SaleId);
            if (saleById != null)
            {
                saleById.PeriodId = saleDTO.PeriodId;
                saleById.VendorId = saleDTO.VendorId;
                saleById.ProductId = saleDTO.ProductId;
                saleById.NumberOfProducts = saleDTO.NumberOfProducts;
                _ = _applicationDBContext.SaveChanges();
            };
        }
    }
}
