using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public ProductRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void AddProduct(ProductDTO productDTO)
        {
            Product newProduct = new()
            {
                ProductName = productDTO.ProductName,
            };
            _ = _applicationDBContext.Products.Add(newProduct);
            _ = _applicationDBContext.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            Product productById = _applicationDBContext.Products.FirstOrDefault(x => x.ProductId == productId);
            if (productById != null)
            {
                _ = _applicationDBContext.Products.Remove(productById);
                _ = _applicationDBContext.SaveChanges();
            }
        }

        public IList<ProductDTO> GetAllProducts()
        {
            IList<ProductDTO> productDTOs = _applicationDBContext.Products.Select(product => new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
            }).ToList();
            return productDTOs;
        }

        public ProductDTO? GetProductById(int productId)
        {
            Product productById = _applicationDBContext.Products.FirstOrDefault(x => x.ProductId == productId);
            return productById == null
                ? null
                : new ProductDTO
                {
                    ProductId = productById.ProductId,
                    ProductName = productById.ProductName
                };
        }

        public void UpdateProduct(ProductDTO productDTO)
        {
            Product productById = _applicationDBContext.Products.FirstOrDefault(x => x.ProductId == productDTO.ProductId);
            if (productById != null)
            {
                productById.ProductName = productDTO.ProductName;
                _ = _applicationDBContext.SaveChanges();
            }
        }
    }
}
