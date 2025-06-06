using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.ValueObjects;

public class PhoneNumber : ValueObject
{
    public const int MaxLength = 20;
    private static readonly Regex E164Regex = new(@"^\+[1-9]\d{6,18}$");

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired("Phone number");

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueTooLong("Phone number", MaxLength);

        if (!E164Regex.IsMatch(trimmed))
            return Errors.General.ValueIsInvalid("Phone number");

        return new PhoneNumber(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}