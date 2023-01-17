using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Entities;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BT_Remunerare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodLogic _periodLogic;
        private readonly IPeriodControllerHelper<PeriodViewModel, PeriodDTO> _periodControllerHelper;

        public PeriodController(IPeriodLogic periodLogic, IPeriodControllerHelper<PeriodViewModel, PeriodDTO> periodControllerHelper)
        {
            _periodLogic = periodLogic;
            _periodControllerHelper = periodControllerHelper;
        }

        [HttpPost]
        [Route("AddPeriod")]
        public IActionResult AddPeriod([FromBody] PeriodViewModel periodViewModel)
        {
            try
            {
                PeriodDTO periodDTO = _periodControllerHelper.BuildDTO(periodViewModel);
                Response response = _periodLogic.AddPeriod(periodDTO);
                if (response.IsSuccesful)
                {
                    return Ok();
                }
                return StatusCode(500, (response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpPost]
        [Route("DeletePeriod")]
        public IActionResult DeletePeriod(int periodId)
        {
            try
            {
                Response response = _periodLogic.DeletePeriod(periodId);
                if (response.IsSuccesful)
                {
                    return Ok();
                }
                return StatusCode(500, (response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpGet]
        [Route("GetAllPeriods")]
        public IActionResult GetAllPeriods()
        {
            try
            {
                IList<PeriodDTO> periodDTOs = _periodLogic.GetAllPeriods();
                IList<PeriodViewModel> periodViewModels = _periodControllerHelper.BuildListViewModel(periodDTOs);
                return Ok(periodViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpGet]
        [Route("GetAllPeriodsWithSalesAndRemuneration")]
        public IActionResult GetAllPeriodsWithSalesAndRemuneration()
        {
            try
            {
                IList<PeriodDTO> periodDTOs = _periodLogic.GetAllPeriodsWithSalesAndRemuneration();
                IList<PeriodViewModel> periodViewModels = _periodControllerHelper.BuildListViewModelWithSalesAndRemuneration(periodDTOs);
                return Ok(periodViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpGet]
        [Route("GetPeriodById")]
        public IActionResult GetPeriodById(int periodId)
        {
            try
            {
                PeriodDTO periodDTO = _periodLogic.GetPeriodById(periodId);
                PeriodViewModel periodViewModel = _periodControllerHelper.BuildViewModel(periodDTO);
                return Ok(periodViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpPost]
        [Route("UpdatePeriod")]
        public IActionResult UpdatePeriod([FromBody] PeriodViewModel periodViewModel)
        {
            try
            {
                PeriodDTO periodDTO = _periodControllerHelper.BuildDTO(periodViewModel);
                Response response = _periodLogic.UpdatePeriod(periodDTO);
                if (response.IsSuccesful)
                {
                    return Ok();
                }
                return StatusCode(500, (response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }
    }
}
