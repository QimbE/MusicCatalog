using FluentValidation;

namespace Application.Genres.Update;

public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(g => g.Name).NotEmpty().MaximumLength(100);
    }
}