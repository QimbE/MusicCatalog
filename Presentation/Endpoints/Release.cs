using Application.ExceptionHandling.ExpectedErrors;
using Application.Releases.Create;
using Application.Releases.Get;
using Application.Releases.Update;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class Release : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Releases");

        group.MapGet("{id:guid}", async ([FromRoute] Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetReleaseQuery(id), cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        });

        group.MapPost("", async ([FromBody] CreateReleaseCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
                );
        });

        group.MapPut("", async ([FromBody] UpdateReleaseCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success"})),
                errors => sender.Send(new HandleErrorQuery(errors))
                );
        });
    }
}