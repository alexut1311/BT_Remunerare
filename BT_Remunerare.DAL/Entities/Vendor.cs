using System.ComponentModel.DataAnnotations;

namespace BT_Remunerare.DAL.Entities
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }
        [Required]
        public string? VendorName { get; set; }
        public virtual ICollection<Sale>? Sales { get; set; }

    }
}
