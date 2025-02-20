namespace Domain.Entity.TeacherTab;

public record TeacherTabId(Guid Value)
{
    public static TeacherTabId New() => new(Guid.NewGuid());
    public static TeacherTabId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}