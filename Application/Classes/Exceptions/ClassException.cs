using Domain.Entity;

namespace Application.Classes.Exceptions;

public class ClassException(ClassId id, string message, Exception? innerException = null)
    : Exception(message,innerException)
{
    public ClassId ClassId { get; } = id;
}

public class ClassNotFoundException(ClassId id) : ClassException(id, $"Lesson under id:{id} Not Found");
public class ClassAlreadExistsException(ClassId id): ClassException(id, $"Lesson already exists: {id}");

public class UserClassNotFoundException : ClassException
{
    public UserClassNotFoundException(UserId userId)
        :base(ClassId.Empty(), $"Teacher with id {userId} was not found.") { }
}
public class ClassUknownException(ClassId id, Exception innerException) 
    :ClassException(id, $"Uknown exception for the lesson under id:{id}", innerException);