using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Application.Features.VolunteerFeature.Models;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworksCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("id"));
        RuleForEach(x => x.SocialNetworks).SetValidator(new SocialNetworkModelValidator());
    }
}