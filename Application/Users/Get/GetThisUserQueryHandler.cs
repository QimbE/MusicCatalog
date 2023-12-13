using Application.Common;
using Application.Data;
using Application.DTO;
using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Get;

public class GetThisUserQueryHandler: IRequestHandler<GetThisUserQuery, ResultType<ThisUserDTO>>
{
    private readonly IApplicationDbContext _context;

    public GetThisUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultType<ThisUserDTO>> Handle(GetThisUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(u => u.Id == request.Id)
            .Select(u => new ThisUserDTO(u.Id, u.Username, u.RoleId))
            .SingleOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            return new UserNotFoundException(nameof(request.Id));
        }

        return user;
    }
}