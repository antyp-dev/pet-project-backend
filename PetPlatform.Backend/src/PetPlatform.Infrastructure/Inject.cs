using Microsoft.Extensions.DependencyInjection;
using PetPlatform.Application.Features.VolunteerFeature;
using PetPlatform.Infrastructure.Repositories;

namespace PetPlatform.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
}