using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
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
        public void AddProduct(ProductViewModel productViewModel)
        {
            ProductDTO productDTO = _productControllerHelper.BuildDTO(productViewModel);
            _productLogic.AddProduct(productDTO);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public void DeleteProduct(int productId)
        {
            _productLogic.DeleteProduct(productId);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IList<ProductViewModel> GetAllProducts()
        {
            IList<ProductDTO> productDTOs = _productLogic.GetAllProducts();
            IList<ProductViewModel> productViewModels = _productControllerHelper.BuildListViewModel(productDTOs);
            return productViewModels;
        }

        [HttpGet]
        [Route("GetProductById")]
        public ProductViewModel? GetProductById(int productId)
        {
            ProductDTO productDTO = _productLogic.GetProductById(productId);
            ProductViewModel productViewModel = _productControllerHelper.BuildViewModel(productDTO);
            return productViewModel;
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public void UpdateProduct(ProductViewModel productViewModel)
        {
            ProductDTO productDTO = _productControllerHelper.BuildDTO(productViewModel);
            _productLogic.UpdateProduct(productDTO);
        }
    }
}
