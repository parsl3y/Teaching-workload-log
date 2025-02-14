namespace Domain.Entity;

public record ClassId(Guid Value)
{
    public static ClassId New() => new(Guid.NewGuid());
    public static ClassId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}