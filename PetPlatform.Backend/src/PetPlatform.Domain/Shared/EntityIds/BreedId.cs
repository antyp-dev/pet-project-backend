using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.EntityIds;

public class BreedId : ValueObject, IComparable<BreedId>
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId NewId() => Guid.NewGuid();
    public static BreedId Empty() => Guid.Empty;
    public static BreedId Create(Guid id) => id;

    public static implicit operator BreedId(Guid id) => new BreedId(id);

    public static implicit operator Guid(BreedId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return id.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(BreedId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}