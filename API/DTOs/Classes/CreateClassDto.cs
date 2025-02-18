using Domain.Entity;

namespace API.DTOs;

public record CreateClassDto(
    Guid? Id,
    string ClassName,
    int TotalClassNumber,
    Guid? TeacherId,
    DateTime ClassDate)
{
    public static CreateClassDto FromDomainModel(Class classes)
        => new (
            classes.Id.Value, 
            classes.ClassName, 
            classes.TotalClassNumber,
            classes.TeacherId?.Value, 
            classes.ClassDate
        );
}