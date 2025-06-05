using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.EntityIds;

public class VolunteerId : ValueObject, IComparable<VolunteerId>
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerId NewId() => Guid.NewGuid();
    public static VolunteerId Empty() => Guid.Empty;
    public static VolunteerId Create(Guid id) => id;

    public static implicit operator VolunteerId(Guid id) => new VolunteerId(id);

    public static implicit operator Guid(VolunteerId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return id.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(VolunteerId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}