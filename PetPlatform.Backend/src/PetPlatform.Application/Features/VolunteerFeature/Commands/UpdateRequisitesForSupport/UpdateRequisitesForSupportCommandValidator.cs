using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Application.Features.VolunteerFeature.Models;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateRequisitesForSupport;

public class UpdateRequisitesForSupportCommandValidator :  AbstractValidator<UpdateRequisitesForSupportCommand>
{
    public UpdateRequisitesForSupportCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("id"));
        
        RuleForEach(x => x.RequisitesForSupport)
            .SetValidator(new RequisiteForSupportModelValidator());
    }
}