using System.Reflection;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class Artist : Entity
{
    public string Name { get; private set; }

    public string? Description { get; private set; }
    
    private Artist()
    {
        
    }
    
    public Artist(Guid id, string name, string? description = null)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}