using Application.ExceptionHandling.ExpectedErrors;
using Application.Genres.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class Genre:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Genres").WithTags("Genres");

        group.MapPost("", async ([FromBody] CreateGenreCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success"})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        });
    }
}