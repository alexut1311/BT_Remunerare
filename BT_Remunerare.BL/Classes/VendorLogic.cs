using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Classes
{
    public class VendorLogic : IVendorLogic
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorLogic(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public void AddVendor(VendorDTO vendorDTO)
        {
            _vendorRepository.AddVendor(vendorDTO);
        }

        public void DeleteVendor(int vendorId)
        {
            _vendorRepository.DeleteVendor(vendorId);
        }

        public IList<VendorDTO> GetAllVendors()
        {
            return _vendorRepository.GetAllVendors();
        }

        public VendorDTO? GetVendorById(int vendorId)
        {
            return _vendorRepository.GetVendorById(vendorId);
        }

        public void UpdateVendor(VendorDTO vendorDTO)
        {
            _vendorRepository.UpdateVendor(vendorDTO);
        }
    }
}
