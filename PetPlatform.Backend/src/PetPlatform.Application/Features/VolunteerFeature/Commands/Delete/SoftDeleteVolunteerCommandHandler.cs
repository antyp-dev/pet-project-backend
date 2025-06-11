using CSharpFunctionalExtensions;
using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.Delete;

public class SoftDeleteVolunteerCommandHandler
{
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public SoftDeleteVolunteerCommandHandler(IValidator<DeleteVolunteerCommand> validator,
        IVolunteerRepository volunteerRepository)
    {
        _validator = validator;
        _volunteerRepository = volunteerRepository;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteVolunteerCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();
        
        var volunteerResult = await _volunteerRepository.GetById(request.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        volunteerResult.Value.SoftDelete();
        
        return await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
    }
}