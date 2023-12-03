using Application.ExceptionHandling.ExpectedErrors;
using Application.Users.Update;
using Carter;
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
        });
        
    }
}