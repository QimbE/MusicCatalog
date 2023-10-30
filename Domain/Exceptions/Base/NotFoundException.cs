namespace Domain.Exceptions.Base;

[Serializable]
public class NotFoundException: Exception
{
    public NotFoundException(string requestedObject, string propertyName)
        :base($"There is no {requestedObject} with such {propertyName}.")
    {
        
    }

    public NotFoundException()
        : base()
    {
        
    }

    public NotFoundException(string requestedObject, string propertyName, Exception inner)
        :base($"There is no {requestedObject} with such {propertyName}.", inner)
    {
        
    }
}