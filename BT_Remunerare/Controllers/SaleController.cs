using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
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
        public IActionResult AddSale([FromBody] SaleViewModel saleViewModel)
        {
            try
            {
                SaleDTO saleDTO = _saleControllerHelper.BuildDTO(saleViewModel);
                Response response = _saleLogic.AddSale(saleDTO);
                return response.IsSuccesful ? Ok() : StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Route("DeleteSale/{saleId}")]
        public IActionResult DeleteSale(int saleId)
        {
            try
            {
                Response response = _saleLogic.DeleteSale(saleId);
                return response.IsSuccesful ? Ok() : StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllSales")]
        public IActionResult GetAllSales()
        {
            try
            {
                IList<SaleDTO> saleDTOs = _saleLogic.GetAllSales();
                IList<SaleViewModel> saleViewModels = _saleControllerHelper.BuildListViewModel(saleDTOs);
                return Ok(saleViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllSalesWithVendorAndProductAndPeriod")]
        public IActionResult GetAllSalesWithVendorAndProductAndPeriod()
        {
            try
            {
                IList<SaleDTO> saleDTOs = _saleLogic.GetAllSales();
                IList<SaleViewModel> saleViewModels = _saleControllerHelper.BuildListViewModelWithVendorAndProductAndPeriod(saleDTOs);
                return Ok(saleViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetSaleById/{saleId}")]
        public IActionResult GetSaleById(int saleId)
        {
            try
            {
                SaleDTO saleDTO = _saleLogic.GetSaleById(saleId);
                SaleViewModel saleViewModel = _saleControllerHelper.BuildViewModel(saleDTO);
                return Ok(saleViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateSale")]
        public IActionResult UpdateSale([FromBody] SaleViewModel saleViewModel)
        {
            try
            {
                SaleDTO saleDTO = _saleControllerHelper.BuildDTO(saleViewModel);
                Response response = _saleLogic.UpdateSale(saleDTO);
                return response.IsSuccesful ? Ok() : StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }
    }
}
