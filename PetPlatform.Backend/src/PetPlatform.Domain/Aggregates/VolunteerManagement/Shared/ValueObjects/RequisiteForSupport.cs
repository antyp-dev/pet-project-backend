using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;

public class RequisiteForSupport : ValueObject
{
    public const int MaxTitleLength = 100;
    public const int MaxDescriptionLength = 1000;

    private RequisiteForSupport(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }

    public static RequisiteForSupport Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        var trimmedTitle = title.Trim();
        var trimmedDescription = description.Trim();

        if (trimmedTitle.Length > MaxTitleLength)
            throw new ArgumentException($"Title must not exceed {MaxTitleLength} characters.", nameof(title));

        if (trimmedDescription.Length > MaxDescriptionLength)
            throw new ArgumentException($"Description must not exceed {MaxDescriptionLength} characters.",
                nameof(description));

        return new RequisiteForSupport(trimmedTitle, trimmedDescription);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title.ToLowerInvariant();
        yield return Description.ToLowerInvariant();
    }
}