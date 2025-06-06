using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;

public class Description : ValueObject
{
    public const int MaxLength = 2000;

    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired("Description");

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueTooLong("Description", MaxLength);

        return new Description(trimmed);
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}