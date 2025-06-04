using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.ValueObjects;

public class BirthDate : ValueObject
{
    public static readonly DateOnly MinDate = new(1900, 1, 1);

    private BirthDate(DateOnly value)
    {
        Value = value;
    }

    public DateOnly Value { get; }

    public static BirthDate Create(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (date > today)
            throw new ArgumentOutOfRangeException(nameof(date), "Birth date cannot be in the future.");

        if (date < MinDate)
            throw new ArgumentOutOfRangeException(nameof(date), $"Birth date cannot be earlier than {MinDate}.");

        return new BirthDate(date);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}