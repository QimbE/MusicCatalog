using System.ComponentModel.DataAnnotations;

namespace Domain.Exceptions;

[Serializable]
public class ValidationFailureException: ValidationException
{
    /// <summary>
    /// Collection of validation errors
    /// </summary>
    public IEnumerable<PropertyError> Errors { get; protected set; }
    
    public ValidationFailureException(IEnumerable<PropertyError> errors) 
        :base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }
    /// <summary>
    /// Map errors to basic exception message.
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    private static string BuildErrorMessage(IEnumerable<PropertyError> errors)
    {
        var arr = errors.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}");
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}

[Serializable]
public class PropertyError
{
    public string PropertyName { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public PropertyError()
    {
        
    }

    public PropertyError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
    public override string ToString()
    {
        return ErrorMessage;
    }
}