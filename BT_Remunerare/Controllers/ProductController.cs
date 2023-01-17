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
    public class ProductController : ControllerBase
    {
        private readonly IProductLogic _productLogic;
        private readonly IControllerHelper<ProductViewModel, ProductDTO> _productControllerHelper;

        public ProductController(IProductLogic productLogic, IControllerHelper<ProductViewModel, ProductDTO> productControllerHelper)
        {
            _productLogic = productLogic;
            _productControllerHelper = productControllerHelper;
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                ProductDTO productDTO = _productControllerHelper.BuildDTO(productViewModel);
                Response response = _productLogic.AddProduct(productDTO);
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
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                Response response = _productLogic.DeleteProduct(productId);

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
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                IList<ProductDTO> productDTOs = _productLogic.GetAllProducts();
                IList<ProductViewModel> productViewModels = _productControllerHelper.BuildListViewModel(productDTOs);
                return Ok(productViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetProductById(int productId)
        {
            try
            {
                ProductDTO productDTO = _productLogic.GetProductById(productId);
                ProductViewModel productViewModel = _productControllerHelper.BuildViewModel(productDTO);
                return Ok(productViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new Response { IsSuccesful = false, ErrorMessage = ex.Message }));
            }
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(ProductViewModel productViewModel)
        {
            try
            {
                ProductDTO productDTO = _productControllerHelper.BuildDTO(productViewModel);
                Response response = _productLogic.UpdateProduct(productDTO);
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
