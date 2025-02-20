using Application.TeacherTabs.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors;

public static class TeacherTabErrorHandler
{
    public static ObjectResult ToObjectResult(this TeacherTabException e)
    {
        return new ObjectResult(e.Message)
        {
            StatusCode = e switch
            {
                DocumentationNotFoundExceptions => StatusCodes.Status404NotFound,
                DocumentationUnknownExceptions => StatusCodes.Status500InternalServerError,
                LessonInDocumentationNotFoundException => StatusCodes.Status404NotFound,
                DocumentOfThisClassAlreadyExists => StatusCodes.Status409Conflict,
                _ => throw new NotImplementedException()
            }
        };
    }
}