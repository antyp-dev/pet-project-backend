using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;

public class Description : ValueObject
{
    public const int MaxLength = 2000;

    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Description Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Description must not exceed {MaxLength} characters.",
                nameof(value));

        return new Description(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}