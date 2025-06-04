using CSharpFunctionalExtensions;

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

    public static SocialNetwork Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Social network name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Social network URL cannot be empty.", nameof(url));

        var trimmedName = name.Trim();
        var trimmedUrl = url.Trim();

        if (trimmedName.Length > MaxNameLength)
            throw new ArgumentException($"Name must not exceed {MaxNameLength} characters.", nameof(name));

        if (trimmedUrl.Length > MaxUrlLength)
            throw new ArgumentException($"URL must not exceed {MaxUrlLength} characters.", nameof(url));

        if (!Uri.IsWellFormedUriString(trimmedUrl, UriKind.Absolute))
            throw new ArgumentException("URL is not valid.", nameof(url));

        return new SocialNetwork(trimmedName, trimmedUrl);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name.ToLowerInvariant();
        yield return Url.ToLowerInvariant();
    }
}