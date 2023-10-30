using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ExpectedErrorHandling;

public record HandleErrorQuery(List<Exception> Errors): IRequest<IResult>;