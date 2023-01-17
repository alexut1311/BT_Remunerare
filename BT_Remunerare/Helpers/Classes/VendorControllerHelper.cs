using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.Helpers.Classes
{
    public class VendorControllerHelper : IControllerHelper<VendorViewModel, VendorDTO>
    {
        public VendorDTO BuildDTO(VendorViewModel vendorViewModel)
        {
            return new VendorDTO
            {
                VendorId = vendorViewModel.VendorId,
                VendorName = vendorViewModel.VendorName,
            };
        }

        public IList<VendorViewModel> BuildListViewModel(IList<VendorDTO> vendorDTOs)
        {
            return vendorDTOs.Select(vendorDTO => new VendorViewModel
            {
                VendorId = vendorDTO.VendorId,
                VendorName = vendorDTO.VendorName,
            }).ToList();
        }

        public VendorViewModel BuildViewModel(VendorDTO? vendorDTO)
        {
            return new VendorViewModel
            {
                VendorId = vendorDTO.VendorId,
                VendorName = vendorDTO.VendorName,
            };
        }
    }
}
