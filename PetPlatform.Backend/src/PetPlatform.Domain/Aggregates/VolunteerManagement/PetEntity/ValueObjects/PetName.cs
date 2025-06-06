using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class PetName : ValueObject
{
    public const int MinLength = 2;
    public const int MaxLength = 30;

    private PetName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PetName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));

        if (name.Length < MinLength)
            throw new ArgumentException($"Name must be at least {MinLength} characters long.", nameof(name));

        if (name.Length > MaxLength)
            throw new ArgumentException($"Name must not exceed {MaxLength} characters.", nameof(name));

        if (!Regex.IsMatch(name, @"^[А-Яа-яЁёA-Za-z\s\-']+$"))
            throw new ArgumentException("Name may contain only letters, spaces, hyphens, and apostrophes.",
                nameof(name));

        return new PetName(name.Trim());
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}