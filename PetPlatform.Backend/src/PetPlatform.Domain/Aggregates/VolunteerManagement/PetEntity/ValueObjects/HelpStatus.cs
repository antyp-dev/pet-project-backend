using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class HelpStatus : ValueObject
{
    public const int MaxLength = 20;

    private HelpStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static readonly HelpStatus NeedsHelp = new("NeedsHelp");
    public static readonly HelpStatus LookingForHome = new("LookingForHome");
    public static readonly HelpStatus Adopted = new("Adopted");

    private static readonly IReadOnlyDictionary<string, HelpStatus> _all =
        new[] { NeedsHelp, LookingForHome, Adopted }
            .ToDictionary(x => x.Value.ToLowerInvariant());

    public static HelpStatus FromString(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Help status cannot be empty.", nameof(input));

        var normalized = input.Trim().ToLowerInvariant();

        if (!_all.TryGetValue(normalized, out var status))
            throw new ArgumentException($"Unknown help status: '{input}'", nameof(input));

        return status;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}