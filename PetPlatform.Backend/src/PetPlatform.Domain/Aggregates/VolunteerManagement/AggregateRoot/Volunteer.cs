using CSharpFunctionalExtensions;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        YearsOfExperience yearsOfExperience,
        PhoneNumber phoneNumber,
        SocialNetworkList socialNetworks,
        RequisiteForSupportList requisitesForSupport) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
        RequisitesForSupport = requisitesForSupport;
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Description Description { get; private set; }
    public YearsOfExperience YearsOfExperience { get; private set; }

    public int CountAdoptedPets() => _pets.Count(p => p.HelpStatus == HelpStatus.Adopted);
    public int CountPetsLookingForHome() => _pets.Count(p => p.HelpStatus == HelpStatus.LookingForHome);
    public int CountPetsNeedHelp() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedsHelp);

    public PhoneNumber PhoneNumber { get; private set; }
    public SocialNetworkList SocialNetworks { get; private set; }
    public RequisiteForSupportList RequisitesForSupport { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;


    public void UpdateMainInfo(
        FullName fullName,
        Description description,
        YearsOfExperience yearsOfExperience,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
    }

    public Result<bool, Error> UpdateSocialNetworks(SocialNetworkList socialNetworks)
    {
        if (socialNetworks.SocialNetworks != null)
        {
            var duplicate = socialNetworks.SocialNetworks
                .GroupBy(s => s.Name.Trim().ToLowerInvariant())
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicate != null)
            {
                return Errors.General.DuplicateValue("SocialNetwork.Name", duplicate.Key);
            }
        }

        SocialNetworks = socialNetworks;
        return true;
    }

    public Result<bool, Error> UpdateRequisitesForSupport(RequisiteForSupportList requisitesForSupport)
    {
        if (requisitesForSupport.Requisites != null)
        {
            var duplicate = requisitesForSupport.Requisites
                .GroupBy(r => r.Title.Trim().ToLowerInvariant())
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicate != null)
            {
                return Errors.General.DuplicateValue("RequisiteForSupport.Title", duplicate.Key);
            }
        }

        RequisitesForSupport = requisitesForSupport;
        return true;
    }
}