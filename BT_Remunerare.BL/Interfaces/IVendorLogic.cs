using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface IVendorLogic
    {
        Response AddVendor(VendorDTO vendorDTO);
        VendorDTO? GetVendorById(int vendorId);
        Response UpdateVendor(VendorDTO vendorDTO);
        Response DeleteVendor(int vendorId);
        IList<VendorDTO> GetAllVendors();
    }
}
