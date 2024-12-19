namespace Tn.Inventory.Domain.Primitives;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        return GetEqualityComponents()
            .SequenceEqual(
                ((ValueObject)obj).GetEqualityComponents());
    }

    private static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            return false;

        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    protected abstract void CheckPolicies();
}