using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories;
using Talabat.Reposatory;

namespace Talabat.APIs.Exstensions
{
    public static class ApplicationServicesExstension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services) 
        {
            
            
            Services.AddScoped(typeof(IBasketRepo) , typeof(BasketRepo));
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles)); 
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //Model State => Dic[KeyValuePair]
                    //Key =>Name Of Parameter
                    //Value =>Errors

                    var erroes = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                     .SelectMany(E => E.Value.Errors)
                                                     .Select(E => E.ErrorMessage)
                                                     .ToList();

                    var ValidationErrorResponses = new ApiValidationsErrorResponse(){Errors = erroes};

                    return new BadRequestObjectResult(ValidationErrorResponses);
                };
            });
            return Services;
        }
        
    }
}
