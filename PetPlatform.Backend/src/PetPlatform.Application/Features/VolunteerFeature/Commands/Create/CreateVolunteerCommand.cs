using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.Create;

public record CreateVolunteerCommand
{
    public FullNameModel FullName { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public string PhoneNumber { get; set; }
    public List<SocialNetworkModel> SocialNetworks { get; set; } = [];
    public List<RequisiteForSupportModel> RequisitesForSupport { get; set; } = [];
}