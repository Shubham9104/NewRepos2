using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GroceryManagement.web.Areas.Identity.IdentityHostingStartup))]
namespace GroceryManagement.web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}