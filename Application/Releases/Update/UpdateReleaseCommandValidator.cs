using Application.Releases.Create;
using FluentValidation;

namespace Application.Releases.Update;

public class UpdateReleaseCommandValidator: AbstractValidator<UpdateReleaseCommand>
{
    public UpdateReleaseCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(r => r.AuthorId)
            .NotEmpty();

        RuleFor(r => r.Description)
            .NotNull();

        RuleFor(r => r.ReleaseDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(r => r.LinkToCover)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(r => r.TypeId)
            .NotEmpty()
            .Must(CreateReleaseCommandValidator.BeAValidTypeId)
            .WithMessage(x => $"There is no Release Type with id {x.TypeId}");
    }
}