using AutoMapper;
using PetPlatform.Application.Common.Mappings;
using PetPlatform.Application.Features.VolunteerFeature.Commands.Create;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateMainInfo;
using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.API.Features.Volunteer.Requests;

public record UpdateMainInfoRequest : IMapWith<UpdateMainInfoCommand>
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public string PhoneNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateMainInfoRequest, UpdateMainInfoCommand>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => new FullNameModel
            {
                LastName = src.LastName,
                FirstName = src.FirstName,
                MiddleName = src.MiddleName
            }));
    }
}