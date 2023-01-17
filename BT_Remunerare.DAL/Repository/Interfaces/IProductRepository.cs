using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface IProductRepository
    {
        Response AddProduct(ProductDTO productDTO);
        ProductDTO? GetProductById(int productId);

        Response UpdateProduct(ProductDTO productDTO);
        Response DeleteProduct(int productId);
        IList<ProductDTO> GetAllProducts();
    }
}
