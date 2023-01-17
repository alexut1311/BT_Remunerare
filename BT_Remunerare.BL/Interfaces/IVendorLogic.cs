using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface IVendorLogic
    {
        void AddVendor(VendorDTO vendorDTO);
        VendorDTO? GetVendorById(int vendorId);
        void UpdateVendor(VendorDTO vendorDTO);
        void DeleteVendor(int vendorId);
        IList<VendorDTO> GetAllVendors();
    }
}
