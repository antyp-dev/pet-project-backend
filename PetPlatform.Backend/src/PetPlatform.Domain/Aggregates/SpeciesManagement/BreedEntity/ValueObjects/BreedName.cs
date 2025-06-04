using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.SpeciesManagement.BreedEntity.ValueObjects;

public class BreedName : ValueObject
{
    public const int MaxLength = 100;
    private static readonly Regex AllowedPattern = new(@"^[А-Яа-яЁёA-Za-z\s\-']+$");

    private BreedName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static BreedName Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Breed name cannot be empty.", nameof(input));

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Breed name must not exceed {MaxLength} characters.", nameof(input));

        if (!AllowedPattern.IsMatch(trimmed))
            throw new ArgumentException("Breed name contains invalid characters.", nameof(input));

        return new BreedName(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}