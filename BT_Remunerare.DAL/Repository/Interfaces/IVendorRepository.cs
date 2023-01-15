using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface IVendorRepository
    {
        void AddVendor(VendorDTO vendorDTO);
        VendorDTO? GetVendorById(int vendorId);
        void UpdateVendor(VendorDTO vendorDTO);
        void DeleteVendor(int vendorId);
        IList<VendorDTO> GetAllVendors();
    }
}
