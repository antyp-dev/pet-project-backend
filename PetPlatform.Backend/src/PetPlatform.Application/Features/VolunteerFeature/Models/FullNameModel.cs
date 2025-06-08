namespace PetPlatform.Application.Features.VolunteerFeature.Models;

public record FullNameModel
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
}