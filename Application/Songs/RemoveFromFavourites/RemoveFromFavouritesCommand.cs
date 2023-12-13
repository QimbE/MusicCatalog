using Application.Common;
using MediatR;

namespace Application.Songs.RemoveFromFavourites;

public record RemoveFromFavouritesCommand(Guid UserId, Guid SongId):IRequest<ResultType<bool>>;

public record RemoveFromFavouritesRequest(Guid Id);