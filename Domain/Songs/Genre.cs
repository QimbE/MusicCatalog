using Domain.Primitives;

namespace Domain.Songs;

public class Genre: Entity
{
    public string Name { get; protected set; }
    
    public List<Song> Songs { get; protected set; }

    protected Genre()
    {
        
    }
    
    protected Genre(string name): 
        base(Guid.NewGuid())
    {
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }

    public static Genre Create(string name)
    {
        return new Genre(name);
    }
}