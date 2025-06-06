using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetPlatform.Application.Features.VolunteerFeature.Commands.CreateVolunteer;

namespace PetPlatform.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerCommandHandler>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}