using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Reposatory.Identity;
using Talabat.Service;

namespace Talabat.APIs.Exstensions
{
    public static class IdentityServicesExstentions
    {
        public static IServiceCollection IdentityServices(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(ITokenServices), typeof(TokenServices));
            Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>();


            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            return Services;
        }
    }
}
