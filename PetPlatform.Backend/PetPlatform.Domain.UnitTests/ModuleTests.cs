using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Domain.UnitTests;

public class ModuleTests
{
    private Volunteer CreateVolunteerWithPets(int count)
    {
        var volunteer = new Volunteer(
            VolunteerId.NewId(),
            FullName.Create("Test", "User").Value,
            Email.Create("email@gmail.com").Value,
            Description.Create("Test").Value,
            YearsOfExperience.Create(1).Value,
            PhoneNumber.Create("+111111111").Value,
            new SocialNetworkList(new List<SocialNetwork>()),
            new RequisiteForSupportList(new List<RequisiteForSupport>())
        );

        for (int i = 0; i < count; i++)
        {
            var pet = CreatePet();
            volunteer.AddPet(pet);
        }

        return volunteer;
    }

    private Pet CreatePet()
    {
        return new Pet(
            PetId.NewId(),
            PetName.Create($"Pet").Value,
            SpeciesId.NewId(),
            Description.Create("Desc").Value,
            BreedId.NewId(),
            FurColor.Create("Brown").Value,
            HealthInfo.Create("Healthy", false, false, null).Value,
            Address.Create("Some city", "street", "house number").Value,
            Weight.CreateFromKilograms(10).Value,
            Height.CreateFromCentimeters(20).Value,
            PhoneNumber.Create("+1234567890").Value,
            true,
            BirthDate.Create(new DateOnly(2000, 1, 1)).Value,
            HelpStatus.NeedsHelp,
            DateTime.UtcNow
        );
    }

    [Fact]
    public void AddPet_Should_Set_Position_Correctly()
    {
        var volunteer = CreateVolunteerWithPets(2);
        var newPet = CreatePet();

        var result = volunteer.AddPet(newPet);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, newPet.Position.Value);
    }

    [Fact]
    public void MovePet_Should_Move_Forward_And_Shift_Others_Back()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petToMove = volunteer.Pets[0]; // position 1
        var result = volunteer.MovePet(petToMove, Position.Create(3).Value);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, petToMove.Position.Value);
        Assert.Equal(1, volunteer.Pets[1].Position.Value); // was 2, now 1
        Assert.Equal(2, volunteer.Pets[2].Position.Value); // was 3, now 2
    }

    [Fact]
    public void MovePet_Should_Move_Backward_And_Shift_Others_Forward()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petToMove = volunteer.Pets[2]; // position 3
        var result = volunteer.MovePet(petToMove, Position.Create(1).Value);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, petToMove.Position.Value);
        Assert.Equal(2, volunteer.Pets[0].Position.Value); // was 1, now 2
        Assert.Equal(3, volunteer.Pets[1].Position.Value); // was 2, now 3
    }

    [Fact]
    public void MovePet_SamePosition_Should_Not_Change_Other_Positions()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petToMove = volunteer.Pets[1]; // position 2
        var result = volunteer.MovePet(petToMove, Position.Create(2).Value);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, petToMove.Position.Value);
        Assert.Equal(1, volunteer.Pets[0].Position.Value);
        Assert.Equal(3, volunteer.Pets[2].Position.Value);
    }

    [Fact]
    public void MovePet_Should_Adjust_OutOfRange_Position()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petToMove = volunteer.Pets[1]; // position 2

        var result = volunteer.MovePet(petToMove, Position.Create(100).Value);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, petToMove.Position.Value); // stays at same logical position
        Assert.Equal(1, volunteer.Pets[0].Position.Value);
        Assert.Equal(3, volunteer.Pets[2].Position.Value);
    }
}