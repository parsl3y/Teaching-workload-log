using Domain.Entity;
using MongoDB.Driver;

namespace API.DTOs;

public record FullClassDto(
    Guid? Id,
    string ClassName,
    int TotlaClassNumber,
    int ClassNumberToday,
    Guid? TeacherId,
    DateTime ClassDate)
{
    public static FullClassDto FromDomainModel(Class classes)
        => new (
            classes.Id.Value,
            classes.ClassName, 
            classes.TotalClassNumber,
            classes.ClassNumberToday,
            classes.TeacherId?.Value, 
            classes.ClassDate
            );
}