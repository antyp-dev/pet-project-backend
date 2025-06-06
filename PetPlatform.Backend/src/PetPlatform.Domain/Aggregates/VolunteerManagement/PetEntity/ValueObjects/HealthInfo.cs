using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

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

    public static Result<HealthInfo, Error> Create(
        string summary,
        bool isVaccinated,
        bool hasChronicDiseases,
        DateOnly? lastVetVisit)
    {
        if (string.IsNullOrWhiteSpace(summary))
            return Errors.General.ValueIsRequired("Health summary");

        var trimmed = summary.Trim();

        if (trimmed.Length > MaxSummaryLength)
            return Errors.General.ValueTooLong("Health summary", MaxSummaryLength);

        var today = DateOnly.FromDateTime(DateTime.Today);
        if (lastVetVisit is { } date && date > today)
            return Errors.General.ValueIsInFuture("Last vet visit", today);

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