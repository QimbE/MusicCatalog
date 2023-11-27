using Domain.Releases;
using FluentValidation;

namespace Application.Releases.Create;

public class CreateReleaseCommandValidator: AbstractValidator<CreateReleaseCommand>
{
    public CreateReleaseCommandValidator()
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
            .Must(BeAValidTypeId)
            .WithMessage(x => $"There is no Release Type with id {x.TypeId}");
    }

    public static bool BeAValidTypeId(int typeId)
    {
        return ReleaseType.TryFromValue(typeId, out var _);
    }
}