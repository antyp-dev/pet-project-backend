using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.EntityIds;

public class PetId : ValueObject, IComparable<PetId>
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId NewId() => Guid.NewGuid();
    public static PetId Empty() => Guid.Empty;
    public static PetId Create(Guid id) => id;

    public static implicit operator PetId(Guid id) => new PetId(id);

    public static implicit operator Guid(PetId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return id.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(PetId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}