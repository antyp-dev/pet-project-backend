using CSharpFunctionalExtensions;
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

    public CreateVolunteerCommandHandler(
        IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerCommand request, CancellationToken cancellationToken)
    {
        // лень писать обработку result, так как потом будет добавлен fluentvalidation
        var fullName = FullName.Create(request.LastName, request.FirstName, request.MiddleName).Value;
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

        var result = await _volunteerRepository.GetByEmail(email, cancellationToken);
        if (result.IsSuccess)
            return Errors.General.AlreadyExists("Volunteer", request.Email);

        var volunteerId = VolunteerId.NewId();
        var volunteer = new Volunteer(volunteerId, fullName, email, description, yearsOfExperience,
            phoneNumber, socialNetworkList, requisiteList);

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}