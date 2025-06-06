using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

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

    public static Result<SpeciesName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired("Species name");

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueTooLong("Species name", MaxLength);

        if (!AllowedPattern.IsMatch(trimmed))
            return Errors.General.ValueIsInvalid("Species name");

        return new SpeciesName(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}