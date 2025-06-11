using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateMainInfo;

public class UpdateMainInfoCommandHandler
{
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateMainInfoCommandHandler> _logger;

    public UpdateMainInfoCommandHandler(
        IValidator<UpdateMainInfoCommand> validator,
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateMainInfoCommandHandler> logger)
    {
        _validator = validator;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteerResult = await _volunteerRepository.GetById(request.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var fullName = FullName.Create(
            request.FullName.LastName, request.FullName.FirstName, request.FullName.MiddleName).Value;
        var description = Description.Create(request.Description).Value;
        var yearsOfExperience = YearsOfExperience.Create(request.YearsOfExperience).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        volunteerResult.Value.UpdateMainInfo(fullName, description, yearsOfExperience, phoneNumber);

        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

        return result;
    }
}