using Domain.Entity;

namespace API.DTOs;

public record IDClassDto(
    Guid? Id)
    
{
    public static IDClassDto FromDomainModel(Class classes)
        => new (
            classes.Id.Value
        );
}