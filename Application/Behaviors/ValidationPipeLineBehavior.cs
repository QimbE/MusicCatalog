using Application.Common;
using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationPipeLineBehavior<TRequest, TResult>: IPipelineBehavior<TRequest, Result<TResult>> 
    where TRequest: IRequest<Result<TResult>>
{
    private readonly Mapper _mapper;
    private readonly IValidator<TRequest> _validator;

    public ValidationPipeLineBehavior(IValidator<TRequest> validator, Mapper mapper)
    {
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<TResult>> Handle(TRequest request, RequestHandlerDelegate<Result<TResult>> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            return new ValidationFailureException(
                _mapper.MapToErrors(validationResult.Errors)
                );
        }

        return await next();
    }
}