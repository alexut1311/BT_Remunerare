using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface IVendorRepository
    {
        Response AddVendor(VendorDTO vendorDTO);
        VendorDTO? GetVendorById(int vendorId);
        Response UpdateVendor(VendorDTO vendorDTO);
        Response DeleteVendor(int vendorId);
        IList<VendorDTO> GetAllVendors();
    }
}
