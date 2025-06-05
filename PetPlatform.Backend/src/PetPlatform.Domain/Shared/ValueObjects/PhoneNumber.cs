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

    public static PhoneNumber Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Phone number cannot be empty.", nameof(input));

        var trimmed = input.Trim();

        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Phone number must not exceed {MaxLength} characters.", nameof(input));

        if (!E164Regex.IsMatch(trimmed))
            throw new ArgumentException("Phone number must be in valid E.164 format (e.g. +12024567041).",
                nameof(input));

        return new PhoneNumber(trimmed);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}