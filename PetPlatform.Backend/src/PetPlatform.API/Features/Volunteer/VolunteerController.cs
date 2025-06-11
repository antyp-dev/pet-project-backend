using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetPlatform.API.Features.Volunteer.Requests;
using PetPlatform.API.Response;
using PetPlatform.Application.Features.VolunteerFeature.Commands.Create;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateMainInfo;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateRequisitesForSupport;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateSocialNetworks;

namespace PetPlatform.API.Features.Volunteer;

[ApiController]
[Route("[controller]")]
public class VolunteerController(IMapper mapper) : BaseController(mapper)
{
    [HttpPost]
    public async Task<EndpointResult<Guid>> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteerCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateVolunteerCommand>(request);
        return await handler.Handle(command, cancellationToken);
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<EndpointResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateMainInfoCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateMainInfoCommand>(request);
        command.Id = id;
        return await handler.Handle(command, cancellationToken);
    }

    [HttpPatch("{id:guid}/social-networks")]
    public async Task<EndpointResult<Guid>> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] UpdateSocialNetworksRequest request,
        [FromServices] UpdateSocialNetworksCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateSocialNetworksCommand>(request);
        command.Id = id;
        return await handler.Handle(command, cancellationToken);
    }

    [HttpPatch("{id:guid}/requisites-for-support")]
    public async Task<EndpointResult<Guid>> UpdateRequisitesForSupport(
        [FromRoute] Guid id,
        [FromBody] UpdateRequisitesForSupportRequest request,
        [FromServices] UpdateRequisitesForSupportCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateRequisitesForSupportCommand>(request);
        command.Id = id;
        return await handler.Handle(command, cancellationToken);
    }
}