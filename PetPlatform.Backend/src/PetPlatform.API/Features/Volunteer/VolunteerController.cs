using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetPlatform.API.Features.Volunteer.Requests;
using PetPlatform.Application.Features.VolunteerFeature.Commands.CreateVolunteer;

namespace PetPlatform.API.Features.Volunteer;

[ApiController]
[Route("[controller]")]
public class VolunteerController(IMapper mapper) : BaseController(mapper)
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerCommandHandler handler,
        [FromServices] IValidator<CreateVolunteerCommand> validator,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateVolunteerCommand>(request);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}