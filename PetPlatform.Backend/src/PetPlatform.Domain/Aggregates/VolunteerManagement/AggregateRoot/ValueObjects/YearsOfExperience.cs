using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;

public class YearsOfExperience : ValueObject
{
    public const int MinValue = 0;
    public const int MaxValue = 100;

    private YearsOfExperience(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<YearsOfExperience, Error> Create(int years)
    {
        if (years < MinValue)
            return Errors.General.ValueTooSmall("Experience", MinValue);

        if (years > MaxValue)
            return Errors.General.ValueTooLarge("Experience", MaxValue);

        return new YearsOfExperience(years);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}