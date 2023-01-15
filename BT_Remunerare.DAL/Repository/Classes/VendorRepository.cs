using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public VendorRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void AddVendor(VendorDTO vendorDTO)
        {
            Vendor newVendor = new()
            {
                VendorName = vendorDTO.VendorName,
            };
            _ = _applicationDBContext.Vendors.Add(newVendor);
            _ = _applicationDBContext.SaveChanges();
        }

        public void DeleteVendor(int vendorId)
        {
            Vendor vendorById = _applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == vendorId);
            if (vendorById != null)
            {
                _ = _applicationDBContext.Vendors.Remove(vendorById);
                _ = _applicationDBContext.SaveChanges();
            }
        }

        public IList<VendorDTO> GetAllVendors()
        {
            IList<VendorDTO> vendorDTOs = _applicationDBContext.Vendors.Select(vendor => new VendorDTO
            {
                VendorId = vendor.VendorId,
                VendorName = vendor.VendorName,
            }).ToList();
            return vendorDTOs;
        }

        public VendorDTO? GetVendorById(int vendorId)
        {
            Vendor vendorById = _applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == vendorId);
            return vendorById == null
                ? null
                : new VendorDTO
                {
                    VendorId = vendorById.VendorId,
                    VendorName = vendorById.VendorName
                };
        }

        public void UpdateVendor(VendorDTO vendorDTO)
        {
            Vendor vendorById = _applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == vendorDTO.VendorId);
            if (vendorById != null)
            {
                vendorById.VendorName = vendorDTO.VendorName;
                _ = _applicationDBContext.SaveChanges();
            }
        }
    }
}
