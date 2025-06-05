using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.ValueObjects;

public class Address : ValueObject
{
    public const int MaxCityLength = 100;
    public const int MaxStreetLength = 100;
    public const int MaxHouseLength = 20;
    public const int MaxAdditionalLength = 100;

    private Address(string city, string street, string houseNumber, string? additionalInfo)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        AdditionalInfo = additionalInfo;
    }

    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }
    public string? AdditionalInfo { get; }

    public static Result<Address, Error> Create(string city, string street, string houseNumber, string? additionalInfo = null)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired("City");

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired("Street");

        if (string.IsNullOrWhiteSpace(houseNumber))
            return Errors.General.ValueIsRequired("House number");

        city = city.Trim();
        street = street.Trim();
        houseNumber = houseNumber.Trim();
        additionalInfo = additionalInfo?.Trim();

        if (city.Length > MaxCityLength)
            return Errors.General.ValueTooLong("City", MaxCityLength);

        if (street.Length > MaxStreetLength)
            return Errors.General.ValueTooLong("Street", MaxStreetLength);

        if (houseNumber.Length > MaxHouseLength)
            return Errors.General.ValueTooLong("House number", MaxHouseLength);

        if (!string.IsNullOrEmpty(additionalInfo) && additionalInfo.Length > MaxAdditionalLength)
            return Errors.General.ValueTooLong("Additional info", MaxAdditionalLength);

        return new Address(city, street, houseNumber, additionalInfo);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return City.ToLowerInvariant();
        yield return Street.ToLowerInvariant();
        yield return HouseNumber.ToLowerInvariant();
        yield return AdditionalInfo?.ToLowerInvariant() ?? string.Empty;
    }
}