using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
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
        public void AddSalesRemuneration([FromBody] SalesRemunerationRuleViewModel saleRemunerationViewModel)
        {
            SalesRemunerationRuleDTO saleRemunerationDTO = _saleRemunerationControllerHelper.BuildDTO(saleRemunerationViewModel);
            _saleRemunerationLogic.AddSalesRemuneration(saleRemunerationDTO);
        }

        [HttpPost]
        [Route("DeleteSalesRemuneration")]
        public void DeleteSalesRemuneration(int saleRemunerationId)
        {
            _saleRemunerationLogic.DeleteSalesRemuneration(saleRemunerationId);
        }

        [HttpGet]
        [Route("GetAllSalesRemunerations")]
        public IList<SalesRemunerationRuleViewModel> GetAllSalesRemunerations()
        {
            IList<SalesRemunerationRuleDTO> saleRemunerationDTOs = _saleRemunerationLogic.GetAllSalesRemunerationRules();
            IList<SalesRemunerationRuleViewModel> saleRemunerationViewModels = _saleRemunerationControllerHelper.BuildListViewModel(saleRemunerationDTOs);
            return saleRemunerationViewModels;
        }

        [HttpGet]
        [Route("GetAllSalesRemunerationsWithProductAndPeriod")]
        public IList<SalesRemunerationRuleViewModel> GetAllSalesRemunerationsWithProductAndPeriod()
        {
            IList<SalesRemunerationRuleDTO> saleRemunerationDTOs = _saleRemunerationLogic.GetAllSalesRemunerationRules();
            IList<SalesRemunerationRuleViewModel> saleRemunerationViewModels = _saleRemunerationControllerHelper.BuildListViewModelWithProductAndPeriod(saleRemunerationDTOs);
            return saleRemunerationViewModels;
        }

        [HttpGet]
        [Route("GetSalesRemunerationById")]
        public SalesRemunerationRuleViewModel? GetSalesRemunerationById(int saleRemunerationId)
        {
            SalesRemunerationRuleDTO saleRemunerationDTO = _saleRemunerationLogic.GetSalesRemunerationById(saleRemunerationId);
            SalesRemunerationRuleViewModel saleRemunerationViewModel = _saleRemunerationControllerHelper.BuildViewModel(saleRemunerationDTO);
            return saleRemunerationViewModel;
        }

        [HttpPost]
        [Route("UpdateSalesRemuneration")]
        public void UpdateSalesRemuneration([FromBody] SalesRemunerationRuleViewModel saleRemunerationViewModel)
        {
            SalesRemunerationRuleDTO saleRemunerationDTO = _saleRemunerationControllerHelper.BuildDTO(saleRemunerationViewModel);
            _saleRemunerationLogic.UpdateSalesRemuneration(saleRemunerationDTO);
        }
    }
}
