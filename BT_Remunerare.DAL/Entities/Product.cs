using System.ComponentModel.DataAnnotations;

namespace BT_Remunerare.DAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public virtual ICollection<SalesRemunerationRule>? SalesRemunerationRules { get; set; }
    }
}
