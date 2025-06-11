using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Models;

public record FullNameModel
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
}

public class FullNameModelValidator : AbstractValidator<FullNameModel>
{
    public FullNameModelValidator()
    {
        RuleFor(x => x)
            .MustBeValueObject(n => FullName.Create(n.LastName, n.FirstName, n.MiddleName));
    }
}