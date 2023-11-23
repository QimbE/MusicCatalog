using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ExceptionHandling.ExpectedErrors;

public record HandleErrorQuery(List<Exception> Errors): IRequest<IResult>;