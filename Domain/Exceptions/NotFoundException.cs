namespace Domain.Exceptions;

[Serializable]
public class NotFoundException: Exception
{
    public PropertyError Property { get; set; }
    
    public NotFoundException(string requestedObject, string propertyName)
        :base($"There is no {requestedObject} with such {propertyName}.")
    {
        Property = new PropertyError(propertyName, this.Message);
    }

    public NotFoundException()
        : base()
    {
        
    }

    public NotFoundException(string requestedObject, string propertyName, Exception inner)
        :base($"There is no {requestedObject} with such {propertyName}.", inner)
    {
        Property = new PropertyError(propertyName, this.Message);
    }
}