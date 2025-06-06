using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class Height : ValueObject
{
    public const int MinCm = 5;
    public const int MaxCm = 250;

    private Height(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<Height, Error> CreateFromCentimeters(int centimeters)
    {
        if (centimeters < MinCm)
            return Errors.General.ValueTooSmall("Pet height", MinCm);

        if (centimeters > MaxCm)
            return Errors.General.ValueTooLarge("Pet height", MaxCm);

        return new Height(centimeters);
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}