using Domain.Exceptions;

namespace Domain.Releases.Exceptions;

public class ReleaseNotFoundException: NotFoundException
{
    
        private const string EntityName = "Release";
        public ReleaseNotFoundException(string propertyName)
            :base(EntityName, propertyName)
        {
        
        }
    
        public ReleaseNotFoundException(string propertyName, Exception inner)
            :base(EntityName, propertyName, inner)
        {
        
        }
    
}