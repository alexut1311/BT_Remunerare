using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.Helpers.Classes
{
    public class SaleControllerHelper : ISaleControllerHelper<SaleViewModel, SaleDTO>
    {
        public SaleDTO BuildDTO(SaleViewModel saleViewModel)
        {
            return new SaleDTO
            {
                SaleId = saleViewModel.SaleId,
                PeriodId = saleViewModel.PeriodId,
                VendorId = saleViewModel.VendorId,
                ProductId = saleViewModel.ProductId,
                NumberOfProducts = saleViewModel.NumberOfProducts,
            };
        }

        public IList<SaleViewModel> BuildListViewModel(IList<SaleDTO> saleDTOs)
        {
            return saleDTOs.Select(saleDTO => new SaleViewModel
            {
                SaleId = saleDTO.SaleId,
                PeriodId = saleDTO.PeriodId,
                VendorId = saleDTO.VendorId,
                ProductId = saleDTO.ProductId,
                NumberOfProducts = saleDTO.NumberOfProducts,
            }).ToList();
        }

        public IList<SaleViewModel> BuildListViewModelWithVendorAndProductAndPeriod(IList<SaleDTO> saleDTOs)
        {
            return saleDTOs.Select(saleDTO => new SaleViewModel
            {
                SaleId = saleDTO.SaleId,
                PeriodId = saleDTO.PeriodId,
                VendorId = saleDTO.VendorId,
                ProductId = saleDTO.ProductId,
                NumberOfProducts = saleDTO.NumberOfProducts,
                SaleVendor = saleDTO.SaleVendor != null ? new VendorViewModel
                {
                    VendorId = saleDTO.SaleVendor.VendorId,
                    VendorName = saleDTO.SaleVendor.VendorName,
                } : null,
                SaleProduct = saleDTO.SaleProduct != null ? new ProductViewModel
                {
                    ProductId = saleDTO.SaleProduct.ProductId,
                    ProductName = saleDTO.SaleProduct.ProductName,
                } : null,
                SalePeriod = saleDTO.SalePeriod != null ? new PeriodViewModel
                {
                    PeriodId = saleDTO.SalePeriod.PeriodId,
                    Year = saleDTO.SalePeriod.Year,
                    Month = saleDTO.SalePeriod.Month,
                } : null,
            }).ToList();
        }

        public TotalSalesViewModel BuildTotalSalesViewModel(TotalSalesDTO totalSalesValueDTO)
        {
            TotalSalesViewModel totalSalesViewModel = new();

            foreach (KeyValuePair<int, IList<VendorTotalSalesDTO>> sale in totalSalesValueDTO.TotalSales)
            {
                IList<VendorTotalSalesViewModel> vendorTotalSalesViewModel = new List<VendorTotalSalesViewModel>();
                foreach (VendorTotalSalesDTO vendorSale in sale.Value)
                {
                    vendorTotalSalesViewModel.Add(new VendorTotalSalesViewModel
                    {
                        Product = new ProductViewModel { ProductId = vendorSale.Product.ProductId, ProductName = vendorSale.Product.ProductName },
                        Vendor = new VendorViewModel { VendorId = vendorSale.Vendor.VendorId, VendorName = vendorSale.Vendor.VendorName },
                        TotalSalesValue = vendorSale.TotalSalesValue
                    });
                }
                totalSalesViewModel.TotalSales.Add(sale.Key, vendorTotalSalesViewModel.OrderBy(x=>x.Vendor.VendorName).ToList());
            }

            return totalSalesViewModel;
        }

        public SaleViewModel BuildViewModel(SaleDTO? saleDTO)
        {
            return new SaleViewModel
            {
                SaleId = saleDTO.SaleId,
                PeriodId = saleDTO.PeriodId,
                VendorId = saleDTO.VendorId,
                ProductId = saleDTO.ProductId,
                NumberOfProducts = saleDTO.NumberOfProducts,
            };
        }
    }
}
