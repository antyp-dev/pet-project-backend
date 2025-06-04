using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class HealthInfo : ValueObject
{
    public const int MaxSummaryLength = 500;

    private HealthInfo(string summary, bool isVaccinated, bool hasChronicDiseases, DateOnly? lastVetVisit)
    {
        Summary = summary;
        IsVaccinated = isVaccinated;
        HasChronicDiseases = hasChronicDiseases;
        LastVetVisit = lastVetVisit;
    }

    public string Summary { get; }
    public bool IsVaccinated { get; }
    public bool HasChronicDiseases { get; }
    public DateOnly? LastVetVisit { get; }

    public static HealthInfo Create(
        string summary,
        bool isVaccinated,
        bool hasChronicDiseases,
        DateOnly? lastVetVisit)
    {
        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Health summary cannot be empty.", nameof(summary));

        var trimmed = summary.Trim();

        if (trimmed.Length > MaxSummaryLength)
            throw new ArgumentException($"Health summary must not exceed {MaxSummaryLength} characters.",
                nameof(summary));

        if (lastVetVisit is { } date && date > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Last vet visit cannot be in the future.", nameof(lastVetVisit));

        return new HealthInfo(trimmed, isVaccinated, hasChronicDiseases, lastVetVisit);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Summary.ToLowerInvariant();
        yield return IsVaccinated;
        yield return HasChronicDiseases;
        yield return LastVetVisit;
    }
}