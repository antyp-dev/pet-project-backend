using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

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

    public static Result<BreedName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired("Breed name");

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueTooLong("Breed name", MaxLength);

        if (!AllowedPattern.IsMatch(trimmed))
            return Errors.General.ValueIsInvalid("Breed name");

        return new BreedName(trimmed);
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}