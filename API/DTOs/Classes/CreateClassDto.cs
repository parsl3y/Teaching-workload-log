using Domain.Entity;

namespace API.DTOs;

public record CreateClassDto(
    string ClassName,
    int TotlaClassNumber,
    Guid? TeacherId,
    DateTime ClassDate)
{
    public static CreateClassDto FromDomainModel(Class classes)
        => new (
            classes.ClassName, 
            classes.TotalClassNumber,
            classes.TeacherId?.Value, 
            classes.ClassDate
        );
}