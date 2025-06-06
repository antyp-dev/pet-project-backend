using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class FurColor : ValueObject
{
    public const int MaxLength = 100;

    private FurColor(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FurColor, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired("Fur color");

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueTooLong("Fur color", MaxLength);

        return new FurColor(trimmed);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}