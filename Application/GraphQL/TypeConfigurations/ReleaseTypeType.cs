using Domain.Releases;

namespace Application.GraphQL.TypeConfigurations;

public class ReleaseTypeType : EnumType<ReleaseType>
{
    protected override void Configure(IEnumTypeDescriptor<ReleaseType> descriptor)
    {
        descriptor.Value(ReleaseType.Album).Name(ReleaseType.Album.Name.ToUpperInvariant());
        
        descriptor.Value(ReleaseType.Single).Name(ReleaseType.Single.Name.ToUpperInvariant());
        
        descriptor.Value(ReleaseType.Mixtape).Name(ReleaseType.Mixtape.Name.ToUpperInvariant());
    }
}