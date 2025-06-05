using CSharpFunctionalExtensions;

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

    public static Height CreateFromCentimeters(int centimeters)
    {
        if (centimeters < MinCm)
            throw new ArgumentOutOfRangeException(nameof(centimeters), $"Pet height must be at least {MinCm} cm.");

        if (centimeters > MaxCm)
            throw new ArgumentOutOfRangeException(nameof(centimeters), $"Pet height must not exceed {MaxCm} cm.");

        return new Height(centimeters);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}