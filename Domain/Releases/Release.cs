using Domain.Artists;
using Domain.Primitives;
using Domain.Songs;

namespace Domain.Releases;

public class Release: Entity
{
    public Guid AuthorId { get; protected set; }
    
    public Artist Author { get; protected set; }
    
    
    public int TypeId { get; protected set; }
    
    public ReleaseType Type { get; protected set; }
    
    
    public string Name { get; protected set; }
    
    public string? Description { get; protected set; }

    public DateOnly ReleaseDate { get; protected set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public string LinkToCover { get; protected set; }
    
    public List<Song> Songs { get; protected set; }

    private Release()
    {
        
    }

    private Release(string name, string? description, DateOnly releaseDate, string linkToCover, Guid authorId, int typeId)
        :base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        ReleaseDate = releaseDate;
        LinkToCover = linkToCover;
        AuthorId = authorId;
        TypeId = typeId;
    }

    public void Update(string name, string? description, DateTime releaseDate, string linkToCover, Guid authorId, int typeId)
    {
        Name = name;
        Description = description;
        ReleaseDate = DateOnly.FromDateTime(releaseDate);
        LinkToCover = linkToCover;
        AuthorId = authorId;
        TypeId = typeId;
    }

    public static Release Create(string name, string? description, DateTime releaseDate, string linkToCover, Guid authorId, int typeId)
    {
        return new Release(
            name,
            description,
            DateOnly.FromDateTime(releaseDate),
            linkToCover,
            authorId,
            typeId
            );
    }

}