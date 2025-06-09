namespace PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;

public record SocialNetworkList
{
    private SocialNetworkList()
    {
    }

    public SocialNetworkList(IEnumerable<SocialNetwork>? socialNetworks)
    {
        SocialNetworks = socialNetworks?.ToList();
    }

    public IReadOnlyList<SocialNetwork>? SocialNetworks { get; }
}