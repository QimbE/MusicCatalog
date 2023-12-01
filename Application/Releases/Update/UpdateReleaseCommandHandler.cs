using Application.Common;
using Application.Data;
using Domain.Releases.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Releases.Update;

public class UpdateReleaseCommandHandler : IRequestHandler<UpdateReleaseCommand, ResultType<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateReleaseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultType<bool>> Handle(UpdateReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await _context.Releases
            .Where(r => r.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (release is null)
        {
            return new ReleaseNotFoundException(nameof(release.Id));
        }
        
        release.Update(request.Name, request.Description, request.ReleaseDate, request.LinkToCover, request.AuthorId, request.TypeId);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}