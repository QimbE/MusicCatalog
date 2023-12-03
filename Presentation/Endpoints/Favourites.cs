using System.Security.Claims;
using Application.Authorization;
using Application.ExceptionHandling.ExpectedErrors;
using Application.Songs.AddToFavourites;
using Application.Songs.RemoveFromFavourites;
using Carter;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class Favourites:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Favourites").WithTags("Favourites").UserShouldBeAtLeast(Role.Default);

        group.MapPost("", async (HttpRequest request, [FromBody] Guid songId, ISender sender, CancellationToken cancellationToken) =>
        {
            var userIdClaim = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Results.Unauthorized();
            }

            var command = new AddToFavouritesCommand(Guid.Parse(userIdClaim.Value), songId);
            
            var result = await sender.Send(command, cancellationToken);
            
            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        });

        group.MapDelete("", async (HttpRequest request, [FromBody] Guid songId, ISender sender, CancellationToken cancellationToken) =>
        {
            var userIdClaim = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Results.Unauthorized();
            }

            var command = new RemoveFromFavouritesCommand(Guid.Parse(userIdClaim.Value), songId);
            
            var result = await sender.Send(command, cancellationToken);
            
            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        });
    }
}