using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetPlatform.Application.Features.VolunteerFeature.Commands.Create;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateMainInfo;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateRequisitesForSupport;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateSocialNetworks;

namespace PetPlatform.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerCommandHandler>();
        services.AddScoped<UpdateSocialNetworksCommandHandler>();
        services.AddScoped<UpdateRequisitesForSupportCommandHandler>();
        services.AddScoped<UpdateMainInfoCommandHandler>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}