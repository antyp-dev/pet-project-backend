using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class Weight : ValueObject
{
    public const decimal MinKg = 0.1m;
    public const decimal MaxKg = 200m;

    private Weight(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Weight CreateFromKilograms(decimal kilograms)
    {
        if (kilograms < MinKg)
            throw new ArgumentOutOfRangeException(nameof(kilograms), $"Pet weight must be at least {MinKg} kg.");

        if (kilograms > MaxKg)
            throw new ArgumentOutOfRangeException(nameof(kilograms), $"Pet weight must not exceed {MaxKg} kg.");

        return new Weight(decimal.Round(kilograms, 2, MidpointRounding.AwayFromZero));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}