using BT_Remunerare.DAL;
using Microsoft.EntityFrameworkCore;

namespace BT_Remunerare.Helpers
{
    public static class WebHostExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                ApplicationDBContext context = services.GetRequiredService<ApplicationDBContext>();

                context.Database.Migrate();

                new DataSeeder(context).SeedData();
            }
            catch (Exception)
            {

            }

            return app;
        }
    }
}
