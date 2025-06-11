using AutoMapper;
using PetPlatform.API.Features.Volunteer.DTOs;
using PetPlatform.Application.Common.Mappings;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateSocialNetworks;

namespace PetPlatform.API.Features.Volunteer.Requests;

public class UpdateSocialNetworksRequest : IMapWith<UpdateSocialNetworksCommand>
{
    public List<SocialNetworkDto> SocialNetworks { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateSocialNetworksRequest, UpdateSocialNetworksCommand>();
    }
}