using AutoMapper;
using PetPlatform.API.Features.Volunteer.DTOs;
using PetPlatform.Application.Common.Mappings;
using PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateRequisitesForSupport;

namespace PetPlatform.API.Features.Volunteer.Requests;

public class UpdateRequisitesForSupportRequest : IMapWith<UpdateRequisitesForSupportCommand>
{
    public List<RequisiteForSupportDto> RequisitesForSupport { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateRequisitesForSupportRequest, UpdateRequisitesForSupportCommand>();
    }
}