namespace tennismanager.shared.Exceptions.Exceptions.Abstract;

public class EntityException(string message) : Exception(message);

public class EntityNotFoundException(string message) : EntityException(message)
{
    public EntityNotFoundException() : this("Entity not found")
    {
    }
}

public class EntityAlreadyExistsException(string message) : EntityException(message)
{
    public EntityAlreadyExistsException() : this("Entity already exists")
    {
    }
}




