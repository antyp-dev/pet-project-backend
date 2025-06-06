using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(x => x.FullName)
            .MustBeValueObject(n => FullName.Create(n.LastName, n.FirstName, n.MiddleName));
        RuleFor(x => x.Email).MustBeValueObject(Email.Create);
        RuleFor(x => x.Description).MustBeValueObject(Description.Create);
        RuleFor(x => x.YearsOfExperience).MustBeValueObject(YearsOfExperience.Create);
        RuleFor(x => x.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleForEach(x => x.SocialNetworks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Url));
        RuleForEach(x => x.RequisitesForSupport)
            .MustBeValueObject(r => RequisiteForSupport.Create(r.Title, r.Description));
    }
}