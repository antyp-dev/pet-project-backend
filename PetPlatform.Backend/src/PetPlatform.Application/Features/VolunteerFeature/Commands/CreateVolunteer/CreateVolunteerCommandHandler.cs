using CSharpFunctionalExtensions;
using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.CreateVolunteer;

public class CreateVolunteerCommandHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerCommandHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(CreateVolunteerCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var fullName = FullName.Create(
            request.FullName.LastName, request.FullName.FirstName, request.FullName.MiddleName).Value;
        var email = Email.Create(request.Email).Value;
        var description = Description.Create(request.Description).Value;
        var yearsOfExperience = YearsOfExperience.Create(request.YearsOfExperience).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var socialNetworks = request.SocialNetworks
            .Select(sn => SocialNetwork.Create(sn.Name, sn.Url).Value)
            .ToList();
        var socialNetworkList = new SocialNetworkList(socialNetworks);

        var requisites = request.RequisitesForSupport
            .Select(r => RequisiteForSupport.Create(r.Title, r.Description).Value)
            .ToList();
        var requisiteList = new RequisiteForSupportList(requisites);

        var volunteerId = VolunteerId.NewId();
        var volunteer = new Volunteer(volunteerId, fullName, email, description, yearsOfExperience,
            phoneNumber, socialNetworkList, requisiteList);

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}