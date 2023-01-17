using BT_Remunerare.BL.Classes;
using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL;
using BT_Remunerare.DAL.Repository.Classes;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.Helpers.Classes;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;

namespace BT_Remunerare
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            string connectionString = Configuration.GetSection("AppSettings")["DatabaseConnection"];
            _ = services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));
            _ = services.AddAuthentication(IISDefaults.AuthenticationScheme);
            _ = services.AddMvc();
            _ = services.AddHttpContextAccessor();
            _ = services.AddControllersWithViews();
            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BT_Remunerare API", Version = "v1" });
                c.UseInlineDefinitionsForEnums();
            });
        }
        private void RegisterServices(IServiceCollection services)
        {
            _ = services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            RegisterDALDependencies(services);
            RegisterBLDependencies(services);
            RegisterControllerHelpersDependencies(services);
        }

        // Register the DAL Dependencies
        private void RegisterDALDependencies(IServiceCollection services)
        {
            _ = services.AddTransient<IPeriodRepository, PeriodRepository>();
            _ = services.AddTransient<IProductRepository, ProductRepository>();
            _ = services.AddTransient<ISaleRepository, SaleRepository>();
            _ = services.AddTransient<ISalesRemunerationRepository, SalesRemunerationRepository>();
            _ = services.AddTransient<IVendorRepository, VendorRepository>();
        }

        // Register the BL Dependencies
        private void RegisterBLDependencies(IServiceCollection services)
        {
            _ = services.AddTransient<IPeriodLogic, PeriodLogic>();
            _ = services.AddTransient<IProductLogic, ProductLogic>();
            _ = services.AddTransient<ISaleLogic, SaleLogic>();
            _ = services.AddTransient<ISalesRemunerationLogic, SalesRemunerationLogic>();
            _ = services.AddTransient<IVendorLogic, VendorLogic>();
        }

        // Register the Controller Helpers Dependencies
        private void RegisterControllerHelpersDependencies(IServiceCollection services)
        {
            _ = services.AddTransient<IControllerHelper<ProductViewModel, ProductDTO>, ProductControllerHelper>();
            _ = services.AddTransient<IControllerHelper<VendorViewModel, VendorDTO>, VendorControllerHelper>();
            _ = services.AddTransient<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>, PeriodControllerHelper>();
            _ = services.AddTransient<ISaleControllerHelper<SaleViewModel, SaleDTO>, SaleControllerHelper>();
            _ = services.AddTransient<ISalesRemunerationRuleControllerHelper<SalesRemunerationRuleViewModel, SalesRemunerationRuleDTO>, SalesRemunerationControllerHelper>();
        }
    }
}
