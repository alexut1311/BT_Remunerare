using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Classes
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;

        public ProductLogic(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Response AddProduct(ProductDTO productDTO)
        {
            return _productRepository.AddProduct(productDTO);
        }

        public Response DeleteProduct(int productId)
        {
            return _productRepository.DeleteProduct(productId);
        }

        public IList<ProductDTO> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public ProductDTO? GetProductById(int productId)
        {
            return productId == 0 ? null : _productRepository.GetProductById(productId);
        }

        public Response UpdateProduct(ProductDTO productDTO)
        {
            return _productRepository.UpdateProduct(productDTO);
        }
    }
}
