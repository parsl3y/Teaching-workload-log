using Domain.Entity;

namespace API.DTOs;

public record CreateClassDto(
    Guid? Id,
    string ClassName,
    int TotalClassNumber,
    Guid? TeacherId,
    int ClassNumberToday,
    DateTime ClassDate)
{
    public static CreateClassDto FromDomainModel(Class classes)
        => new (
            classes.Id.Value, 
            classes.ClassName, 
            classes.TotalClassNumber,
            classes.TeacherId?.Value, 
            classes.ClassNumberToday,
            classes.ClassDate
        );
}