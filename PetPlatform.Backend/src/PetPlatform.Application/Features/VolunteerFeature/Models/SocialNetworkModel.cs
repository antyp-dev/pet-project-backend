using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Models;

public record SocialNetworkModel
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class SocialNetworkModelValidator : AbstractValidator<SocialNetworkModel>
{
    public SocialNetworkModelValidator()
    {
        RuleFor(x => x)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Url));
    }
}