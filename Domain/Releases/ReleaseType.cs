using Ardalis.SmartEnum;

namespace Domain.Releases;

public class ReleaseType: SmartEnum<ReleaseType>
{
    public static readonly ReleaseType Album = new(nameof(Album), 1);
    public static readonly ReleaseType Single = new(nameof(Single), 2);
    public static readonly ReleaseType Mixtape = new(nameof(Mixtape), 3);
    
    public List<Releases.Release> Releases { get; set; }
    
    private ReleaseType(string name, int value)
        : base(name, value)
    {
        
    }
}