using BT_Remunerare.DAL;
using BT_Remunerare.DAL.Entities;

namespace BT_Remunerare.Helpers
{
    public class DataSeeder
    {
        private readonly ApplicationDBContext _context;
        private IList<Product> _products;
        private IList<Vendor> _vendors;
        private IList<Period> _periods;
        public DataSeeder(ApplicationDBContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Products.Any())
            {
                _products = new List<Product>() {
                new Product
                {
                    ProductName = "Produsul 1",
                },
                new Product
                {
                    ProductName = "Produsul 2",
                },
                new Product
                {
                    ProductName = "Produsul 3",
                },
                new Product
                {
                    ProductName = "Produsul 4",
                }
                };
                foreach(Product product in _products) {
                    _context.Products.Add(product);
                }
            }
            else
            {
                _products = _context.Products.ToList();
            }

            if (!_context.Vendors.Any())
            {
                _vendors = new List<Vendor>() {
                new Vendor
                {
                    VendorName = "Vanzatorul 1",
                },
                new Vendor
                {
                    VendorName = "Vanzatorul 2",
                },
                new Vendor
                {
                    VendorName = "Vanzatorul 3",
                },
                new Vendor
                {
                    VendorName = "Vanzatorul 4",
                }
                };
                foreach (Vendor vendor in _vendors)
                {
                    _context.Vendors.Add(vendor);
                }
            }
            else
            {
                _vendors = _context.Vendors.ToList();
            }

            if (!_context.Periods.Any())
            {
                _periods = new List<Period>() {
                new Period
                {
                    Year = 2023,
                    Month = 1,
                },
                new Period
                {
                    Year = 2023,
                    Month = 2,
                },
                };
                foreach (Period period in _periods)
                {
                    _context.Periods.Add(period);
                }
            }
            else
            {
                _periods = _context.Periods.ToList();
            }

            _context.SaveChanges();

            if (!_context.Sales.Any())
            {
                _context.Sales.AddRange(new List<Sale> {
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[0].VendorId,
                    ProductId= _products[0].ProductId,
                    NumberOfProducts=123
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[0].VendorId,
                    ProductId= _products[1].ProductId,
                    NumberOfProducts=234
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[0].VendorId,
                    ProductId= _products[2].ProductId,
                    NumberOfProducts=345
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[0].VendorId,
                    ProductId= _products[3].ProductId,
                    NumberOfProducts=456
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[1].VendorId,
                    ProductId= _products[0].ProductId,
                    NumberOfProducts=147
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[1].VendorId,
                    ProductId= _products[1].ProductId,
                    NumberOfProducts=258
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[1].VendorId,
                    ProductId= _products[2].ProductId,
                    NumberOfProducts=369
                },
                new Sale
                {
                    PeriodId= _periods[0].PeriodId,
                    VendorId= _vendors[1].VendorId,
                    ProductId= _products[3].ProductId,
                    NumberOfProducts=470
                },
                });
            }

            if (!_context.SalesRemunerationRules.Any())
            {
                _context.SalesRemunerationRules.AddRange(new List<SalesRemunerationRule> {
                new SalesRemunerationRule
                {
                    PeriodId=_periods[0].PeriodId,
                    ProductId=_products[0].ProductId,
                    Remuneration=25
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[0].PeriodId,
                    ProductId=_products[1].ProductId,
                    Remuneration=30
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[0].PeriodId,
                    ProductId=_products[2].ProductId,
                    Remuneration=20
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[0].PeriodId,
                    ProductId=_products[3].ProductId,
                    Remuneration=15
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[1].PeriodId,
                    ProductId=_products[0].ProductId,
                    Remuneration=30
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[1].PeriodId,
                    ProductId=_products[1].ProductId,
                    Remuneration=30
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[1].PeriodId,
                    ProductId=_products[2].ProductId,
                    Remuneration=25
                },
                new SalesRemunerationRule
                {
                    PeriodId=_periods[1].PeriodId,
                    ProductId=_products[3].ProductId,
                    Remuneration=10
                },});
            }
            _context.SaveChanges();

        }
    }
}
