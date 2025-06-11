using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Models;

public record RequisiteForSupportModel
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class RequisiteForSupportModelValidator : AbstractValidator<RequisiteForSupportModel>
{
    public RequisiteForSupportModelValidator()
    {
        RuleFor(x => x)
            .MustBeValueObject(r => RequisiteForSupport.Create(r.Title, r.Description));
    }
}