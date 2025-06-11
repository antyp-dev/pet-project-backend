using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Application.Features.VolunteerFeature.Models;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateMainInfo;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("id"));
        RuleFor(x => x.FullName)
            .SetValidator(new FullNameModelValidator());
        RuleFor(x => x.Description).MustBeValueObject(Description.Create);
        RuleFor(x => x.YearsOfExperience).MustBeValueObject(YearsOfExperience.Create);
        RuleFor(x => x.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}