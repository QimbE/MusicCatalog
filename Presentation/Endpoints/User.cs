using System.Security.Claims;
using Application.Authorization;
using Application.ExceptionHandling.ExpectedErrors;
using Application.Users.Get;
using Application.Users.Update;
using Carter;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class User:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Users").WithTags("Users");

        group.MapPut("", async ([FromBody] UpdateUserCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            
            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        }).UserShouldBeAtLeast(Role.Admin);
        
        group.MapGet("", async (HttpRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var userIdClaim = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Results.Unauthorized();
            }

            var query = new GetThisUserQuery(Guid.Parse(userIdClaim.Value));
            
            var result = await sender.Send(query, cancellationToken);
            
            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        }).UserShouldBeAtLeast(Role.Default);
        
    }
}