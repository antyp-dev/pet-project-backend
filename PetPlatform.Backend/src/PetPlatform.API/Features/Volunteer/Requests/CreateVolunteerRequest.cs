using AutoMapper;
using PetPlatform.API.Features.Volunteer.DTOs;
using PetPlatform.Application.Common.Mappings;
using PetPlatform.Application.Features.VolunteerFeature.Commands.CreateVolunteer;
using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.API.Features.Volunteer.Requests;

public record CreateVolunteerRequest : IMapWith<CreateVolunteerCommand>
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public string PhoneNumber { get; set; }
    public List<SocialNetworkDto> SocialNetworks { get; set; } = [];
    public List<RequisiteForSupportDto> RequisitesForSupport { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateVolunteerRequest, CreateVolunteerCommand>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => new FullNameModel
            {
                LastName = src.LastName,
                FirstName = src.FirstName,
                MiddleName = src.MiddleName
            }));
    }
}