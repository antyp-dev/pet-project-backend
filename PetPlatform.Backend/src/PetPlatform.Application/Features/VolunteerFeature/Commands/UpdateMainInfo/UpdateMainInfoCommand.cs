using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateMainInfo;

public record UpdateMainInfoCommand
{
    public Guid Id { get; set; }
    public FullNameModel FullName { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public string PhoneNumber { get; set; }
}