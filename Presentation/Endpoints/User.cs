using Application.ExceptionHandling.ExpectedErrors;
using Application.Users;
using Application.Users.Login;
using Application.Users.Register;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class User: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("users");

        group.MapPost("login", async ([FromBody] LoginCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(request, cancellationToken);

            return await res.MatchAsync<IResult>(
                result => Task.FromResult(Results.Ok(result)),
                errors => sender.Send(new HandleErrorQuery(errors))
                );
        });

        group.MapPost("register", async ([FromBody] RegisterCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(request, cancellationToken);

            return await res.MatchAsync<IResult>(
                result => Task.FromResult(Results.Ok(result)),
                errors => sender.Send(new HandleErrorQuery(errors))
                );
        });
    }
}