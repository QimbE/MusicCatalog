using Domain.Exceptions;
using FluentValidation.Results;
using Riok.Mapperly.Abstractions;

namespace Application.Common;

[Mapper]
public partial class Mapper
{
    public partial IEnumerable<PropertyError> MapToErrors(IEnumerable<ValidationFailure> failures);
}