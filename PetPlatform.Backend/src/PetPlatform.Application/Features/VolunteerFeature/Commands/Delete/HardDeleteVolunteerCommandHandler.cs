using CSharpFunctionalExtensions;
using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.Delete;

public class HardDeleteVolunteerCommandHandler
{
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public HardDeleteVolunteerCommandHandler(IValidator<DeleteVolunteerCommand> validator,
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
        
        return await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
    }
}