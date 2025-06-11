using CSharpFunctionalExtensions;
using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateRequisitesForSupport;

public class UpdateRequisitesForSupportCommandHandler
{
    private readonly IValidator<UpdateRequisitesForSupportCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdateRequisitesForSupportCommandHandler(
        IValidator<UpdateRequisitesForSupportCommand> validator,
        IVolunteerRepository volunteerRepository)
    {
        _validator = validator;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateRequisitesForSupportCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteerResult = await _volunteerRepository.GetById(request.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var requisites = request.RequisitesForSupport
            .Select(r => RequisiteForSupport.Create(r.Title, r.Description).Value)
            .ToList();
        var requisiteList = new RequisiteForSupportList(requisites);

        var requisitesResult = volunteerResult.Value.UpdateRequisitesForSupport(requisiteList);
        if (requisitesResult.IsFailure)
            return requisitesResult.Error.ToErrorList();

        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

        return result;
    }
}