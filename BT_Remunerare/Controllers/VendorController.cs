using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BT_Remunerare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorLogic _vendorLogic;
        private readonly IControllerHelper<VendorViewModel, VendorDTO> _vendorControllerHelper;
        public VendorController(IVendorLogic vendorLogic, IControllerHelper<VendorViewModel, VendorDTO> vendorControllerHelper)
        {
            _vendorLogic = vendorLogic;
            _vendorControllerHelper = vendorControllerHelper;
        }

        [HttpPost]
        [Route("AddVendor")]
        public void AddVendor(VendorViewModel vendorViewModel)
        {
            VendorDTO vendorDTO = _vendorControllerHelper.BuildDTO(vendorViewModel);
            _vendorLogic.AddVendor(vendorDTO);
        }

        [HttpPost]
        [Route("DeleteVendor")]
        public void DeleteVendor(int vendorId)
        {
            _vendorLogic.DeleteVendor(vendorId);
        }

        [HttpGet]
        [Route("GetAllVendors")]
        public IList<VendorViewModel> GetAllVendors()
        {
            IList<VendorDTO> vendorDTOs = _vendorLogic.GetAllVendors();
            IList<VendorViewModel> vendorViewModels = _vendorControllerHelper.BuildListViewModel(vendorDTOs);
            return vendorViewModels;
        }

        [HttpGet]
        [Route("GetVendorById")]
        public VendorViewModel? GetVendorById(int vendorId)
        {
            VendorDTO vendorDTO = _vendorLogic.GetVendorById(vendorId);
            VendorViewModel vendorViewModel = _vendorControllerHelper.BuildViewModel(vendorDTO);
            return vendorViewModel;
        }

        [HttpPost]
        [Route("UpdateVendor")]
        public void UpdateVendor(VendorViewModel vendorViewModel)
        {
            VendorDTO vendorDTO = _vendorControllerHelper.BuildDTO(vendorViewModel);
            _vendorLogic.UpdateVendor(vendorDTO);
        }
    }
}
