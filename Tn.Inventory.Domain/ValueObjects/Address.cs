using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    public Address(string name, string fullAddress, string district, string city)
    {
        Name = name;
        FullAddress = fullAddress;
        District = district;
        City = city;
    }

    public string Name { get; }
    public string FullAddress { get; }
    public string District { get; }
    public string City { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return FullAddress;
        yield return District;
        yield return City;
    }

    protected override void CheckPolicies()
    {
    }
}