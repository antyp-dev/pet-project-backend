using CSharpFunctionalExtensions;

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

    public static YearsOfExperience Create(int years)
    {
        if (years < MinValue)
            throw new ArgumentOutOfRangeException(nameof(years), $"Experience must be at least {MinValue} years.");

        if (years > MaxValue)
            throw new ArgumentOutOfRangeException(nameof(years), $"Experience must not exceed {MaxValue} years.");

        return new YearsOfExperience(years);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}