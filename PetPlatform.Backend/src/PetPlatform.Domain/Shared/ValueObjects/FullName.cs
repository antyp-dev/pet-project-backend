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

    public static Result<FullName, Error> Create(string lastName, string firstName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired("Last name");

        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired("First name");

        var trimmedLastName = lastName.Trim();
        var trimmedFirstName = firstName.Trim();
        var trimmedMiddleName = middleName?.Trim();

        if (trimmedLastName.Length > MaxPartLength)
            return Errors.General.ValueTooLong("Last name", MaxPartLength);

        if (trimmedFirstName.Length > MaxPartLength)
            return Errors.General.ValueTooLong("First name", MaxPartLength);

        if (!string.IsNullOrEmpty(trimmedMiddleName) && trimmedMiddleName.Length > MaxPartLength)
            return Errors.General.ValueTooLong("Middle name", MaxPartLength);

        return new FullName(trimmedLastName, trimmedFirstName, trimmedMiddleName);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return LastName.ToLowerInvariant();
        yield return FirstName.ToLowerInvariant();
        yield return MiddleName?.ToLowerInvariant() ?? string.Empty;
    }
}