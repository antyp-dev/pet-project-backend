using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

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

    public static Result<Weight, Error> CreateFromKilograms(decimal kilograms)
    {
        if (kilograms < MinKg)
            return Errors.General.ValueTooSmall("Pet weight", (int)MinKg);

        if (kilograms > MaxKg)
            return Errors.General.ValueTooLarge("Pet weight", (int)MaxKg);

        return new Weight(decimal.Round(kilograms, 2, MidpointRounding.AwayFromZero));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}