using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetPlatform.API.Features.Volunteer.Requests;
using PetPlatform.API.Response;
using PetPlatform.Application.Features.VolunteerFeature.Commands.CreateVolunteer;

namespace PetPlatform.API.Features.Volunteer;

[ApiController]
[Route("[controller]")]
public class VolunteerController(IMapper mapper) : BaseController(mapper)
{
    [HttpPost]
    public async Task<EndpointResult<Guid>> Create(
        [FromServices] CreateVolunteerCommandHandler handler,
        [FromServices] IValidator<CreateVolunteerCommand> validator,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateVolunteerCommand>(request);
        return await handler.Handle(command, cancellationToken);
    }
}