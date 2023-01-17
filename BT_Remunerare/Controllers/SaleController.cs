using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BT_Remunerare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleLogic? _saleLogic;
        private readonly ISaleControllerHelper<SaleViewModel, SaleDTO>? _saleControllerHelper;

        public SaleController(ISaleLogic? saleLogic, ISaleControllerHelper<SaleViewModel, SaleDTO>? saleControllerHelper)
        {
            _saleLogic = saleLogic;
            _saleControllerHelper = saleControllerHelper;
        }

        [HttpPost]
        [Route("AddSale")]
        public void AddSale([FromBody] SaleViewModel saleViewModel)
        {
            SaleDTO saleDTO = _saleControllerHelper.BuildDTO(saleViewModel);
            _saleLogic.AddSale(saleDTO);
        }

        [HttpPost]
        [Route("DeleteSale")]
        public void DeleteSale(int saleId)
        {
            _saleLogic.DeleteSale(saleId);
        }

        [HttpGet]
        [Route("GetAllSales")]
        public IList<SaleViewModel> GetAllSales()
        {
            IList<SaleDTO> saleDTOs = _saleLogic.GetAllSales();
            IList<SaleViewModel> saleViewModels = _saleControllerHelper.BuildListViewModel(saleDTOs);
            return saleViewModels;
        }

        [HttpGet]
        [Route("GetAllSalesWithVendorAndProductAndPeriod")]
        public IList<SaleViewModel> GetAllSalesWithVendorAndProductAndPeriod()
        {
            IList<SaleDTO> saleDTOs = _saleLogic.GetAllSales();
            IList<SaleViewModel> saleViewModels = _saleControllerHelper.BuildListViewModelWithVendorAndProductAndPeriod(saleDTOs);
            return saleViewModels;
        }

        [HttpGet]
        [Route("GetSaleById")]
        public SaleViewModel? GetSaleById(int saleId)
        {
            SaleDTO saleDTO = _saleLogic.GetSaleById(saleId);
            SaleViewModel saleViewModel = _saleControllerHelper.BuildViewModel(saleDTO);
            return saleViewModel;
        }

        [HttpPost]
        [Route("UpdateSale")]
        public void UpdateSale([FromBody] SaleViewModel saleViewModel)
        {
            SaleDTO saleDTO = _saleControllerHelper.BuildDTO(saleViewModel);
            _saleLogic.UpdateSale(saleDTO);
        }
    }
}
