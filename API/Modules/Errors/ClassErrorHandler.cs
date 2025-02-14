using Application.Classes.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors;

public static class ClassErrorHandler
{
    public static ObjectResult ToObjectResult(this ClassException e)
    {
        return new ObjectResult(e.Message)
        {
            StatusCode = e switch
            {
                ClassNotFoundException => StatusCodes.Status404NotFound,
                ClassAlreadExistsException => StatusCodes.Status409Conflict,
                ClassUknownException => StatusCodes.Status500InternalServerError,
                UserClassNotFoundException => StatusCodes.Status404NotFound,
                _ => throw new NotImplementedException()
            }
        };
    }
}