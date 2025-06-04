using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.ValueObjects;

public class FullName : ValueObject
{
    public const int MaxPartLength = 100;

    private FullName(string lastName, string firstName, string? middleName)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    public string LastName { get; }
    public string FirstName { get; }
    public string? MiddleName { get; }

    public static FullName Create(string lastName, string firstName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        var trimmedLastName = lastName.Trim();
        var trimmedFirstName = firstName.Trim();
        var trimmedMiddleName = middleName?.Trim();

        if (trimmedLastName.Length > MaxPartLength)
            throw new ArgumentException($"Last name must not exceed {MaxPartLength} characters.", nameof(lastName));

        if (trimmedFirstName.Length > MaxPartLength)
            throw new ArgumentException($"First name must not exceed {MaxPartLength} characters.", nameof(firstName));

        if (!string.IsNullOrEmpty(trimmedMiddleName) && trimmedMiddleName.Length > MaxPartLength)
            throw new ArgumentException($"Middle name must not exceed {MaxPartLength} characters.", nameof(middleName));

        return new FullName(trimmedLastName, trimmedFirstName, trimmedMiddleName);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return LastName.ToLowerInvariant();
        yield return FirstName.ToLowerInvariant();
        yield return MiddleName?.ToLowerInvariant() ?? string.Empty;
    }
}