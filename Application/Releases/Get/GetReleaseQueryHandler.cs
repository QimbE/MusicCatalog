using Application.Common;
using Application.Data;
using Application.DTO.Release;
using Domain.Releases.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Releases.Get;

public class GetReleaseQueryHandler: IRequestHandler<GetReleaseQuery, ResultType<ReleaseResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public GetReleaseQueryHandler(IApplicationDbContext context, ICacheService cache, IMapper mapper)
    {
        _context = context;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<ResultType<ReleaseResponse>> Handle(GetReleaseQuery request, CancellationToken cancellationToken)
    {
        var release = await _cache.GetDataAsync<ReleaseResponse>(CachingKeys.ReleaseResponsePrefix + request.Id, cancellationToken);

        if (release is not null)
        {
            return release;
        }

        release = await _context.Releases
            .Include(x => x.Author)
            .Include(x => x.Songs)
            .ThenInclude(x => x.Genre)
            .Include(x => x.Songs)
            .ThenInclude(x => x.ArtistsOnFeat)
            .Include(x => x.Type)
            .Where( x=> x.Id == request.Id)
            .Select(x => _mapper.MapToResponse(x))
            .FirstOrDefaultAsync(cancellationToken);

        if (release is null)
        {
            return new ReleaseNotFoundException(nameof(request.Id));
        }
        
        await _cache.SetDataAsync(
            CachingKeys.ReleaseResponsePrefix + release.Id,
            release,
            DateTimeOffset.UtcNow.AddMinutes(1),
            cancellationToken
        );

        return release;
    }
}