using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

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

    public static Result<RequisiteForSupport, Error> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Errors.General.ValueIsRequired("Title");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Description");

        var trimmedTitle = title.Trim();
        var trimmedDescription = description.Trim();

        if (trimmedTitle.Length > MaxTitleLength)
            return Errors.General.ValueTooLong("Title", MaxTitleLength);

        if (trimmedDescription.Length > MaxDescriptionLength)
            return Errors.General.ValueTooLong("Description", MaxDescriptionLength);

        return new RequisiteForSupport(trimmedTitle, trimmedDescription);
    }


    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title.ToLowerInvariant();
        yield return Description.ToLowerInvariant();
    }
}