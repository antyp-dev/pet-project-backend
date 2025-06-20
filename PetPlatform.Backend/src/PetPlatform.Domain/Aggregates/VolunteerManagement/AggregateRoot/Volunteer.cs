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
    private bool _isDeleted = false;

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

    public void SoftDelete()
    {
        _isDeleted = true;
        _pets.ForEach(p => p.SoftDelete());
    }

    public void Restore()
    {
        _isDeleted = false;
        _pets.ForEach(p => p.Restore());
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var position = Position.Create(_pets.Count + 1);
        if (position.IsFailure)
            return position.Error;

        pet.SetPosition(position.Value);

        _pets.Add(pet);
        return Result.Success<Error>();
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition || _pets.Count == 1)
            return Result.Success<Error>();

        var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
        if (adjustedPosition.IsFailure)
            return adjustedPosition.Error;

        newPosition = adjustedPosition.Value;

        var moveResult = MoveIssuesBetweenPositions(newPosition, currentPosition);
        if (moveResult.IsFailure)
            return moveResult.Error;

        pet.Move(newPosition);

        newPosition = adjustedPosition.Value;

        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }

    private UnitResult<Error> MoveIssuesBetweenPositions(Position newPosition, Position currentPosition)
    {
        if (newPosition < currentPosition)
        {
            var issuesToMove = _pets.Where(i => i.Position >= newPosition
                                                && i.Position < currentPosition);

            foreach (var issueToMove in issuesToMove)
            {
                var result = issueToMove.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else if (newPosition > currentPosition)
        {
            var issuesToMove = _pets.Where(i => i.Position > currentPosition
                                                && i.Position <= newPosition);

            foreach (var issueToMove in issuesToMove)
            {
                var result = issueToMove.MoveBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        return Result.Success<Error>();
    }
}