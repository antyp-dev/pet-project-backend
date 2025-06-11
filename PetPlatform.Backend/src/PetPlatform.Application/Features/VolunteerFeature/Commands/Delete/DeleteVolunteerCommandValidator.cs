using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.Delete;

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}