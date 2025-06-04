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

    public static Email Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Email cannot be empty.", nameof(input));

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Email must not exceed {MaxLength} characters.", nameof(input));

        if (!EmailRegex.IsMatch(trimmed))
            throw new ArgumentException("Email is not in a valid format.", nameof(input));

        return new Email(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}