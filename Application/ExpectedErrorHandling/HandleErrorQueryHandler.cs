using Domain.Exceptions;
using Domain.Exceptions.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ExpectedErrorHandling;

public class HandleErrorQueryHandler: IRequestHandler<HandleErrorQuery, IResult>
{
    public async Task<IResult> Handle(HandleErrorQuery request, CancellationToken cancellationToken)
    {
        var error = request.Errors[0];

        (int statusCode, string message, IDictionary<string, object?>? additionalInfo) = error switch
        {
            NotFoundException e => (StatusCodes.Status404NotFound, e.Message, null),
            ArtistWithTheSameNameException e => (StatusCodes.Status409Conflict, e.Message, null),
            ValidationFailureException e => ValidationFailureToResponse(e),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.", null)
        };
        
        return Results.Problem(statusCode: statusCode, title: message, extensions: additionalInfo);
    }

    private static (int statusCode, string message, IDictionary<string, object?>? additionalInfo) ValidationFailureToResponse(ValidationFailureException e)
    {
        var info = new Dictionary<string, object?>
        {
            {"Validation Errors", e.Errors}
        };

        return (StatusCodes.Status400BadRequest, e.Message, info);
    }
}