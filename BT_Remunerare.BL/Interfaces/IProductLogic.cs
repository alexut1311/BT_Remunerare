using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface IProductLogic
    {
        void AddProduct(ProductDTO productDTO);
        ProductDTO? GetProductById(int productId);

        void UpdateProduct(ProductDTO productDTO);
        void DeleteProduct(int productId);
        IList<ProductDTO> GetAllProducts();
    }
}
