using AutoMapper;
using PetPlatform.Application.Common.Mappings;
using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.API.Features.Volunteer.DTOs;

public record RequisiteForSupportDto : IMapWith<RequisiteForSupportModel>
{
    public string Title { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RequisiteForSupportDto, RequisiteForSupportModel>();
    }
}