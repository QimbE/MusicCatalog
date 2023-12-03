using Application.Common;
using MediatR;

namespace Application.Songs.AddToFavourites;

public record AddToFavouritesCommand(Guid UserId, Guid SongId):IRequest<ResultType<bool>>;