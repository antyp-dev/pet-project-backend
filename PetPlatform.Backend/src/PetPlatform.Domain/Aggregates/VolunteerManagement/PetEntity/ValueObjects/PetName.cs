using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

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

    public static Result<PetName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");

        if (name.Length < MinLength)
            return Errors.General.ValueTooSmall("Name", MinLength);

        if (name.Length > MaxLength)
            return Errors.General.ValueTooLarge("Name", MaxLength);

        if (!Regex.IsMatch(name, @"^[А-Яа-яЁёA-Za-z\s\-']+$"))
            return Errors.General.ValueIsInvalid("Name");

        return new PetName(name.Trim());
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}