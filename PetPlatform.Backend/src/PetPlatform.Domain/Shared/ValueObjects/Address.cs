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

    public static Address Create(string city, string street, string houseNumber, string? additionalInfo = null)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.", nameof(city));

        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required.", nameof(street));

        if (string.IsNullOrWhiteSpace(houseNumber))
            throw new ArgumentException("House number is required.", nameof(houseNumber));

        city = city.Trim();
        street = street.Trim();
        houseNumber = houseNumber.Trim();
        additionalInfo = additionalInfo?.Trim();

        if (city.Length > MaxCityLength)
            throw new ArgumentException($"City must not exceed {MaxCityLength} characters.", nameof(city));

        if (street.Length > MaxStreetLength)
            throw new ArgumentException($"Street must not exceed {MaxStreetLength} characters.", nameof(street));

        if (houseNumber.Length > MaxHouseLength)
            throw new ArgumentException($"House number must not exceed {MaxHouseLength} characters.",
                nameof(houseNumber));

        if (!string.IsNullOrEmpty(additionalInfo) && additionalInfo.Length > MaxAdditionalLength)
            throw new ArgumentException($"Additional info must not exceed {MaxAdditionalLength} characters.",
                nameof(additionalInfo));

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