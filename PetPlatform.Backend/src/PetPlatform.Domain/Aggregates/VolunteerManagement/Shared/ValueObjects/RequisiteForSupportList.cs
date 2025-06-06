namespace PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;

public record RequisiteForSupportList
{
    private RequisiteForSupportList()
    {
    }

    public RequisiteForSupportList(IEnumerable<RequisiteForSupport> requisites)
    {
        Requisites = requisites.ToList();
    }

    public IReadOnlyList<RequisiteForSupport> Requisites { get; } = [];
}