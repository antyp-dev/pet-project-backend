using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;

public class SocialNetwork : ValueObject
{
    public const int MaxNameLength = 50;
    public const int MaxUrlLength = 500;

    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; }
    public string Url { get; }

    public static Result<SocialNetwork, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");

        if (string.IsNullOrWhiteSpace(url))
            return Errors.General.ValueIsRequired("URL");

        var trimmedName = name.Trim();
        var trimmedUrl = url.Trim();

        if (trimmedName.Length > MaxNameLength)
            return Errors.General.ValueTooLong("Name", MaxNameLength);

        if (trimmedUrl.Length > MaxUrlLength)
            return Errors.General.ValueTooLong("URL", MaxUrlLength);

        if (!Uri.IsWellFormedUriString(trimmedUrl, UriKind.Absolute))
            return Errors.General.ValueIsInvalid("URL");

        return new SocialNetwork(trimmedName, trimmedUrl);
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name.ToLowerInvariant();
        yield return Url.ToLowerInvariant();
    }
}