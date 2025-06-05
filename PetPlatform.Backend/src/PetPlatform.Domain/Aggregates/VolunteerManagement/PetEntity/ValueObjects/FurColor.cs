using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class FurColor : ValueObject
{
    public const int MaxLength = 100;

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

        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Fur color must not exceed {MaxLength} characters.",
                nameof(value));

        return new FurColor(trimmed);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}