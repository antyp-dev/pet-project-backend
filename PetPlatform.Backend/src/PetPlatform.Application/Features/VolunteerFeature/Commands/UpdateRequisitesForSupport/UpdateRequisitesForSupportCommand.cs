using PetPlatform.Application.Features.VolunteerFeature.Models;

namespace PetPlatform.Application.Features.VolunteerFeature.Commands.UpdateRequisitesForSupport;

public class UpdateRequisitesForSupportCommand
{
    public Guid Id { get; set; }
    public List<RequisiteForSupportModel> RequisitesForSupport { get; set; } = [];
}