using Domain.Entity;
using Domain.Entity.TeacherTab;

namespace Application.TeacherTabs.Exceptions;

public class TeacherTabException(TeacherTabId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public TeacherTabId TeacherTabId { get; } = id;
}

public class DocumentOfThisClassAlreadyExists : TeacherTabException
{
    public DocumentOfThisClassAlreadyExists(ClassId classId)
        : base(TeacherTabId.Empty(), $"A TeacherTab for class with id {classId} already exists.") { }
}
    
public class DocumentationNotFoundExceptions(TeacherTabId id) : TeacherTabException(id, $"Documentation under id:{id} Not Found");
public class DocumentationUnknownExceptions(TeacherTabId id, Exception innerException) 
    :TeacherTabException(id, $"Uknown exception for the lesson under id:{id}", innerException);
    
public class LessonInDocumentationNotFoundException : TeacherTabException
{
    public LessonInDocumentationNotFoundException(ClassId classId)
        :base(TeacherTabId.Empty(), $"Game with id {classId} was not found.") { }
}    