using Application.Common;
using Application.Data;
using Domain.Exceptions;
using Domain.Junction;
using Domain.Songs.Exceptions;
using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Songs.AddToFavourites;

public class AddToFavouritesCommandHandler:IRequestHandler<AddToFavouritesCommand, ResultType<bool>>
{
    private readonly IApplicationDbContext _context;

    public AddToFavouritesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ResultType<bool>> Handle(AddToFavouritesCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken))
        {
            return new UserNotFoundException(nameof(request.UserId));
        }

        if (!await _context.Songs.AnyAsync(s => s.Id == request.SongId, cancellationToken))
        {
            return new SongNotFoundException(nameof(request.SongId));
        }
        
        if (await _context.SongUsers.AnyAsync(su => su.SongId == request.SongId && su.UserId == request.UserId, cancellationToken))
        {
            return new SongIsAlreadyInFavouritesException(request.SongId);
        }

        _context.SongUsers.Add(new SongUser(request.SongId, request.UserId));

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}