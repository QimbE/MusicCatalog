using Application.Common;
using Application.Data;
using Domain.Songs.Exceptions;
using Domain.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Songs.RemoveFromFavourites;

public class RemoveFromFavouritesCommandHandler:IRequestHandler<RemoveFromFavouritesCommand, ResultType<bool>>
{
    private readonly IApplicationDbContext _context;

    public RemoveFromFavouritesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultType<bool>> Handle(RemoveFromFavouritesCommand request, CancellationToken cancellationToken)
    {
        var favourite = await _context.SongUsers
            .SingleOrDefaultAsync(su =>
                su.SongId == request.SongId && su.UserId == request.UserId, cancellationToken
                );
        
        if (favourite is null)
        {
            return new SongIsNotInFavouritesException(request.SongId);
        }

        _context.SongUsers.Remove(favourite);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}