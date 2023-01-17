using BT_Remunerare.BL.Classes;
using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Entities;
using BT_Remunerare.Helpers.Classes;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
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
        public IActionResult AddVendor(VendorViewModel vendorViewModel)
        {
            try
            {
                VendorDTO vendorDTO = _vendorControllerHelper.BuildDTO(vendorViewModel);
                Response response = _vendorLogic.AddVendor(vendorDTO);
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
        [Route("DeleteVendor")]
        public IActionResult DeleteVendor(int vendorId)
        {
            try
            {
                Response response = _vendorLogic.DeleteVendor(vendorId);
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
        [Route("GetAllVendors")]
        public IActionResult GetAllVendors()
        {
            try
            {
                IList<VendorDTO> vendorDTOs = _vendorLogic.GetAllVendors();
                IList<VendorViewModel> vendorViewModels = _vendorControllerHelper.BuildListViewModel(vendorDTOs);
                return Ok(vendorViewModels);            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpGet]
        [Route("GetVendorById")]
        public IActionResult GetVendorById(int vendorId)
        {
            try
            {
                VendorDTO vendorDTO = _vendorLogic.GetVendorById(vendorId);
                VendorViewModel vendorViewModel = _vendorControllerHelper.BuildViewModel(vendorDTO);
                return Ok(vendorViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpPost]
        [Route("UpdateVendor")]
        public IActionResult UpdateVendor(VendorViewModel vendorViewModel)
        {
            try
            {
                VendorDTO vendorDTO = _vendorControllerHelper.BuildDTO(vendorViewModel);
                Response response = _vendorLogic.UpdateVendor(vendorDTO);
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
