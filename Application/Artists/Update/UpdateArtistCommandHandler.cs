using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Artists.Update;

public sealed class UpdateArtistCommandHandler: IRequestHandler<UpdateArtistCommand, ResultType<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public UpdateArtistCommandHandler( IUnitOfWork unitOfWork, ICacheService cache, IMapper mapper, IApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ResultType<bool>> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists
            .Where(a => a.Id == request.Id)
            .Include(a => a.Releases)
            .ThenInclude(r=> r.Type)
            .FirstOrDefaultAsync(cancellationToken);

        if (artist is null)
        {
           return new ArtistNotFoundException(nameof(request.Id));
        }

        if (await _context.Artists.AnyAsync(a=> a.Name == request.Name, cancellationToken))
        {
            return new ArtistWithTheSameNameException(request.Name);
        }

        artist.Update(request.Name, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _cache.SetDataAsync(
            CachingKeys.ArtistResponsePrefix + artist.Id,
            _mapper.MapToResponse(artist),
            DateTimeOffset.UtcNow.AddMinutes(1),
            cancellationToken
            );

        return true;
    }
}