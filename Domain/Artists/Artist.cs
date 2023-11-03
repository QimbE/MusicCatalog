using Domain.Primitives;

namespace Domain.Artists;

public sealed class Artist : Entity
{
    /// <summary>
    /// Artsit's name or pseudonym.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Description of an Artist.
    /// </summary>
    public string? Description { get; private set; }
    
    /// <summary>
    /// Creates a new instance of Artis with preinstantiated Id.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="description"><see cref="Description"/></param>
    private Artist(string name, string? description = null)
        : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Updates an instance of Artist based on provided data.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="description"><see cref="Description"/></param>
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Creates a new instance of Artis with preinstantiated Id.
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="description"><see cref="Description"/></param>
    /// <returns>New intance of Artist</returns>
    public static Artist Create(string name, string? description)
    {
        return new Artist(name, description);
    }
}