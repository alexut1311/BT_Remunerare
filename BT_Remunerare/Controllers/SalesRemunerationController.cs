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
    public class SalesRemunerationsController : ControllerBase
    {
        private readonly ISalesRemunerationLogic? _saleRemunerationLogic;
        private readonly ISalesRemunerationRuleControllerHelper<SalesRemunerationRuleViewModel, SalesRemunerationRuleDTO>? _saleRemunerationControllerHelper;

        public SalesRemunerationsController(ISalesRemunerationLogic? saleRemunerationLogic, ISalesRemunerationRuleControllerHelper<SalesRemunerationRuleViewModel, SalesRemunerationRuleDTO>? saleRemunerationControllerHelper)
        {
            _saleRemunerationLogic = saleRemunerationLogic;
            _saleRemunerationControllerHelper = saleRemunerationControllerHelper;
        }

        [HttpPost]
        [Route("AddSalesRemuneration")]
        public IActionResult AddSalesRemuneration([FromBody] SalesRemunerationRuleViewModel saleRemunerationViewModel)
        {
            try
            {
                SalesRemunerationRuleDTO saleRemunerationDTO = _saleRemunerationControllerHelper.BuildDTO(saleRemunerationViewModel);
                Response response = _saleRemunerationLogic.AddSalesRemuneration(saleRemunerationDTO);

                return response.IsSuccesful ? Ok() : StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Route("DeleteSalesRemuneration/{saleRemunerationId}")]
        public IActionResult DeleteSalesRemuneration(int saleRemunerationId)
        {
            try
            {
                Response response = _saleRemunerationLogic.DeleteSalesRemuneration(saleRemunerationId);
                return response.IsSuccesful ? Ok() : StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllSalesRemunerations")]
        public IActionResult GetAllSalesRemunerations()
        {
            try
            {
                IList<SalesRemunerationRuleDTO> saleRemunerationDTOs = _saleRemunerationLogic.GetAllSalesRemunerationRules();
                IList<SalesRemunerationRuleViewModel> saleRemunerationViewModels = _saleRemunerationControllerHelper.BuildListViewModel(saleRemunerationDTOs);
                return Ok(saleRemunerationViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllSalesRemunerationsWithProductAndPeriod")]
        public IActionResult GetAllSalesRemunerationsWithProductAndPeriod()
        {
            try
            {
                IList<SalesRemunerationRuleDTO> saleRemunerationDTOs = _saleRemunerationLogic.GetAllSalesRemunerationRules();
                IList<SalesRemunerationRuleViewModel> saleRemunerationViewModels = _saleRemunerationControllerHelper.BuildListViewModelWithProductAndPeriod(saleRemunerationDTOs);
                return Ok(saleRemunerationViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetSalesRemunerationById/{saleRemunerationId}")]
        public IActionResult GetSalesRemunerationById(int saleRemunerationId)
        {
            try
            {
                SalesRemunerationRuleDTO saleRemunerationDTO = _saleRemunerationLogic.GetSalesRemunerationById(saleRemunerationId);
                SalesRemunerationRuleViewModel saleRemunerationViewModel = _saleRemunerationControllerHelper.BuildViewModel(saleRemunerationDTO);
                return Ok(saleRemunerationViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateSalesRemuneration")]
        public IActionResult UpdateSalesRemuneration([FromBody] SalesRemunerationRuleViewModel saleRemunerationViewModel)
        {
            try
            {
                SalesRemunerationRuleDTO saleRemunerationDTO = _saleRemunerationControllerHelper.BuildDTO(saleRemunerationViewModel);

                Response response = _saleRemunerationLogic.UpdateSalesRemuneration(saleRemunerationDTO);
                return response.IsSuccesful ? Ok() : StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { IsSuccesful = false, ErrorMessage = ex.Message });
            }
        }
    }
}
