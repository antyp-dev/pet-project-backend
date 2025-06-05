using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.ValueObjects;

public class Email : ValueObject
{
    public const int MaxLength = 254;

    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired("Email");

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueTooLong("Email", MaxLength);

        if (!EmailRegex.IsMatch(trimmed))
            return Errors.General.ValueIsInvalid("Email");

        return new Email(trimmed);
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}