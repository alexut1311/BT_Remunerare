using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public void AddPeriod([FromBody] PeriodViewModel periodViewModel)
        {
            PeriodDTO periodDTO = _periodControllerHelper.BuildDTO(periodViewModel);
            _periodLogic.AddPeriod(periodDTO);
        }

        [HttpPost]
        [Route("DeletePeriod")]
        public void DeletePeriod(int periodId)
        {
            _periodLogic.DeletePeriod(periodId);
        }

        [HttpGet]
        [Route("GetAllPeriods")]
        public IList<PeriodViewModel> GetAllPeriods()
        {
            IList<PeriodDTO> periodDTOs = _periodLogic.GetAllPeriods();
            IList<PeriodViewModel> periodViewModels = _periodControllerHelper.BuildListViewModel(periodDTOs);
            return periodViewModels;
        }

        [HttpGet]
        [Route("GetAllPeriodsWithSalesAndRemuneration")]
        public IList<PeriodViewModel> GetAllPeriodsWithSalesAndRemuneration()
        {
            IList<PeriodDTO> periodDTOs = _periodLogic.GetAllPeriodsWithSalesAndRemuneration();
            IList<PeriodViewModel> periodViewModels = _periodControllerHelper.BuildListViewModelWithSalesAndRemuneration(periodDTOs);
            return periodViewModels;
        }

        [HttpGet]
        [Route("GetPeriodById")]
        public PeriodViewModel? GetPeriodById(int periodId)
        {
            PeriodDTO periodDTO = _periodLogic.GetPeriodById(periodId);
            PeriodViewModel periodViewModel = _periodControllerHelper.BuildViewModel(periodDTO);
            return periodViewModel;
        }

        [HttpPost]
        [Route("UpdatePeriod")]
        public void UpdatePeriod([FromBody] PeriodViewModel periodViewModel)
        {
            PeriodDTO periodDTO = _periodControllerHelper.BuildDTO(periodViewModel);
            _periodLogic.UpdatePeriod(periodDTO);
        }
    }
}
