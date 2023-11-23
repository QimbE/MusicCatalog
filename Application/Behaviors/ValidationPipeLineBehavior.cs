using Application.Common;
using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationPipeLineBehavior<TRequest, TResult>: IPipelineBehavior<TRequest, ResultType<TResult>> 
    where TRequest: IRequest<ResultType<TResult>>
{
    private readonly IMapper _mapper;
    private readonly IValidator<TRequest> _validator;

    public ValidationPipeLineBehavior(IValidator<TRequest> validator, IMapper mapper)
    {
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ResultType<TResult>> Handle(TRequest request, RequestHandlerDelegate<ResultType<TResult>> next, CancellationToken cancellationToken)
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