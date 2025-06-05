using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class FurColor : ValueObject
{
    private FurColor(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static FurColor Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Fur color cannot be empty.", nameof(value));

        var trimmed = value.Trim();

        if (trimmed.Length > Constants.MaxLowTextLength)
            throw new ArgumentException($"Fur color must not exceed {Constants.MaxLowTextLength} characters.",
                nameof(value));

        return new FurColor(trimmed);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}