using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.IdentityModel.Tokens;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public ProductRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public Response AddProduct(ProductDTO productDTO)
        {
            try
            {
                Product newProduct = new()
                {
                    ProductName = productDTO.ProductName,
                };
                _ = _applicationDBContext.Products.Add(newProduct);
                _ = _applicationDBContext.SaveChanges();
                return new Response { IsSuccesful = true };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public Response DeleteProduct(int productId)
        {
            try
            {
                Product productById = _applicationDBContext.Products.FirstOrDefault(x => x.ProductId == productId);
                if (productById != null)
                {
                    _ = _applicationDBContext.Products.Remove(productById);
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No product with product id {productId} was found" };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
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

        public Response UpdateProduct(ProductDTO productDTO)
        {
            try
            {
                Product productById = _applicationDBContext.Products.FirstOrDefault(x => x.ProductId == productDTO.ProductId);
                if (productById != null)
                {
                    productById.ProductName = (productById.ProductName == productDTO.ProductName) || productDTO.ProductName.IsNullOrEmpty() ? productById.ProductName : productDTO.ProductName;
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No product with product id {productDTO.ProductId} was found" };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }
    }
}
