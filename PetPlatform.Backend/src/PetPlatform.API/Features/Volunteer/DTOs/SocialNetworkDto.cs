using AutoMapper;
using PetPlatform.Application.Common.Mappings;
using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.API.Features.Volunteer.DTOs;

public record SocialNetworkDto : IMapWith<SocialNetworkModel>
{
    public string Name { get; set; }
    public string Url { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SocialNetworkDto, SocialNetworkModel>();
    }
}