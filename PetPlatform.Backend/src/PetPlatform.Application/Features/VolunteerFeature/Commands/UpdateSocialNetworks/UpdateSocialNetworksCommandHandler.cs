using CSharpFunctionalExtensions;
using FluentValidation;
using PetPlatform.Application.Common.Validation;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworksCommandHandler
{
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdateSocialNetworksCommandHandler(
        IValidator<UpdateSocialNetworksCommand> validator,
        IVolunteerRepository volunteerRepository)
    {
        _validator = validator;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateSocialNetworksCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteerResult = await _volunteerRepository.GetById(request.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var socialNetworks = request.SocialNetworks
            .Select(sn => SocialNetwork.Create(sn.Name, sn.Url).Value)
            .ToList();
        var socialNetworkList = new SocialNetworkList(socialNetworks);

        var socialNetworksResult = volunteerResult.Value.UpdateSocialNetworks(socialNetworkList);
        if (socialNetworksResult.IsFailure)
            return socialNetworksResult.Error.ToErrorList();

        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

        return result;
    }
}