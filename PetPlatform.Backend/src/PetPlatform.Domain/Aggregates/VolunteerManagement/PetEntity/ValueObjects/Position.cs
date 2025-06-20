using CSharpFunctionalExtensions;
using PetPlatform.Domain.Shared;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;

public class Position : ValueObject
{
    public static readonly Position First = new(1);

    private Position(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public Result<Position, Error> Forward()
        => Create(Value + 1);

    public Result<Position, Error> Back()
        => Create(Value - 1);

    public static Result<Position, Error> Create(int position)
    {
        if (position < 1)
            return Errors.General.ValueIsInvalid("position");

        return new Position(position);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Position position) =>
        position.Value;
}