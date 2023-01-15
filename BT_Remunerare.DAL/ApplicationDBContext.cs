using BT_Remunerare.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BT_Remunerare.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<SalesRemunerationRule> SalesRemunerationRules { get; set; }

    }
}
