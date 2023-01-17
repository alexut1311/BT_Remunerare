using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.Helpers.Classes
{
    public class ProductControllerHelper : IControllerHelper<ProductViewModel, ProductDTO>
    {
        public ProductDTO BuildDTO(ProductViewModel productViewModel)
        {
            return new ProductDTO
            {
                ProductId = productViewModel.ProductId,
                ProductName = productViewModel.ProductName,
            };
        }

        public IList<ProductViewModel> BuildListViewModel(IList<ProductDTO> productDTOs)
        {
            return productDTOs.Select(productDTO => new ProductViewModel { ProductId = productDTO.ProductId, ProductName = productDTO.ProductName }).ToList();
        }

        public ProductViewModel BuildViewModel(ProductDTO? productDTO)
        {
            return new ProductViewModel
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
            };
        }
    }
}
