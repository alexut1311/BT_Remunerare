using BT_Remunerare.DAL.Entities;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.IdentityModel.Tokens;

namespace BT_Remunerare.DAL.Repository.Classes
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public VendorRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public Response AddVendor(VendorDTO vendorDTO)
        {
            try
            {
                Vendor newVendor = new()
                {
                    VendorName = vendorDTO.VendorName,
                };
                _ = _applicationDBContext.Vendors.Add(newVendor);
                _ = _applicationDBContext.SaveChanges();
                return new Response { IsSuccesful = true };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }

        public Response DeleteVendor(int vendorId)
        {
            try
            {
                Vendor vendorById = _applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == vendorId);
                if (vendorById != null)

                {
                    _ = _applicationDBContext.Vendors.Remove(vendorById);
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No vendor with vendor id {vendorId} was found" };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
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

        public Response UpdateVendor(VendorDTO vendorDTO)
        {
            try
            {
                Vendor vendorById = _applicationDBContext.Vendors.FirstOrDefault(x => x.VendorId == vendorDTO.VendorId);

                if (vendorById != null)
                {
                    vendorById.VendorName = (vendorById.VendorName == vendorDTO.VendorName) || vendorDTO.VendorName.IsNullOrEmpty() ? vendorById.VendorName : vendorDTO.VendorName;
                    _ = _applicationDBContext.SaveChanges();
                    return new Response { IsSuccesful = true };
                }
                return new Response { IsSuccesful = false, ErrorMessage = $"No vendor with vendor id {vendorDTO.VendorId} was found" };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccesful = false, ErrorMessage = ex.Message };
            }
        }
    }
}
