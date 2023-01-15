using BT_Remunerare.DAL;
using BT_Remunerare.TL.Common;
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
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BT_Remunerare API", Version = "v1" });
                c.UseInlineDefinitionsForEnums();
            });
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            RegisterDALDependencies(services);
            RegisterBLDependencies(services);
        }

        // Register the DAL Dependencies
        private void RegisterDALDependencies(IServiceCollection services)
        {

        }

        // Register the BL Dependencies
        private void RegisterBLDependencies(IServiceCollection services)
        {

        }
    }
}
