using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.SpeciesManagement.AggregateRoot.ValueObjects;

public class SpeciesName : ValueObject
{
    public const int MaxLength = 100;
    private static readonly Regex AllowedPattern = new(@"^[А-Яа-яЁёA-Za-z\s\-']+$");

    private SpeciesName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static SpeciesName Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Species name cannot be empty.", nameof(input));

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Species name must not exceed {MaxLength} characters.", nameof(input));

        if (!AllowedPattern.IsMatch(trimmed))
            throw new ArgumentException("Species name contains invalid characters.", nameof(input));

        return new SpeciesName(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}