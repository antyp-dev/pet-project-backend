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

    public static Result<BirthDate, Error> Create(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (date > today)
            return Errors.General.ValueIsInFuture("Birth date", today);

        if (date < MinDate)
            return Errors.General.ValueIsTooOld("Birth date", MinDate);

        return new BirthDate(date);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}