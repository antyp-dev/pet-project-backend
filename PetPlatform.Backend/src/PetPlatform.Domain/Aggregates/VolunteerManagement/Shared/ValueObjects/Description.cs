using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;

public class Description : ValueObject
{
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

        if (trimmed.Length > Constants.MaxHighTextLength)
            throw new ArgumentException($"Description must not exceed {Constants.MaxHighTextLength} characters.",
                nameof(value));

        return new Description(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}