using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
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

        public void AddProduct(ProductDTO productDTO)
        {
            _productRepository.AddProduct(productDTO);
        }

        public void DeleteProduct(int productId)
        {
            if (productId == 0)
            {
                return;
            }
            _productRepository.DeleteProduct(productId);
        }

        public IList<ProductDTO> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public ProductDTO? GetProductById(int productId)
        {
            return productId == 0 ? null : _productRepository.GetProductById(productId);
        }

        public void UpdateProduct(ProductDTO productDTO)
        {
            _productRepository.UpdateProduct(productDTO);
        }
    }
}
