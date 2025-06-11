using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworksCommand
{
    public Guid Id { get; set; }
    public List<SocialNetworkModel> SocialNetworks { get; set; } = [];
}