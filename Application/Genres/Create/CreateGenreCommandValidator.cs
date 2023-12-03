using FluentValidation;

namespace Application.Genres.Create;

public class CreateGenreCommandValidator: AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(g => g.Name).NotEmpty().MaximumLength(100);
    }
}