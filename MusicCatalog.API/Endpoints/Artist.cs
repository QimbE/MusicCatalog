using Application.Artists.Create;
using Application.Artists.Delete;
using Application.Artists.Get;
using Application.Artists.Update;
using Carter;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MusicCatalog.API.Endpoints;

public class Artist : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("artists", async ([FromBody] CreateArtistCommand command, ISender sender) =>
        {
            try
            {
                await sender.Send(command);
            
                return Results.Ok();
            }
            catch (ArtistWithTheSameNameException e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapGet("artists/{id:guid}", async ([FromRoute] Guid id, ISender sender) =>
        {
            try
            {
                var result = await sender.Send(new GetArtistQuery(id));

                return Results.Ok(new { Message = "Success", Data = result });
            }
            catch (ArtistNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            
        });

        app.MapPut("artists/{id:guid}", async ([FromRoute] Guid id, [FromBody] UpdateArtistRequest request, ISender sender) =>
        {
            try
            {
                var command = new UpdateArtistCommand(id, request.Name, request.Description);

                await sender.Send(command);

                return Results.Ok();
            }
            catch (ArtistNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (ArtistWithTheSameNameException e)
            {
                return Results.BadRequest(e.Message);
            }
            
        });

        app.MapDelete("artists/{id:guid}", async ([FromRoute] Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteArtistCommand(id));
            
                return Results.NoContent();
            }
            catch (ArtistNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}