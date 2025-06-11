using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Application.Features.VolunteerFeature.Models;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.Create;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(x => x.FullName)
            .SetValidator(new FullNameModelValidator());
        RuleFor(x => x.Email).MustBeValueObject(Email.Create);
        RuleFor(x => x.Description).MustBeValueObject(Description.Create);
        RuleFor(x => x.YearsOfExperience).MustBeValueObject(YearsOfExperience.Create);
        RuleFor(x => x.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleForEach(x => x.SocialNetworks)
            .SetValidator(new SocialNetworkModelValidator());
        RuleForEach(x => x.RequisitesForSupport)
            .SetValidator(new RequisiteForSupportModelValidator());
    }
}