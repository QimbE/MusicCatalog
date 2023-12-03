using Application.Songs.Create;
using FluentValidation;

namespace Application.Songs.Update;

public class UpdateSongCommandValidator: AbstractValidator<UpdateSongCommand>
{
    public UpdateSongCommandValidator()
    {
        RuleFor(s => s.ReleaseId).NotEmpty();

        RuleFor(s => s.GenreId).NotEmpty();
            
        RuleFor(s => s.Name).NotEmpty().MaximumLength(150);

        RuleFor(s => s.AudioLink).NotEmpty().MaximumLength(2000);

        RuleFor(s => s.ArtistOnFeatIds).NotNull().Must(CreateSongCommandValidator.NotContainDuplicates).WithMessage("Must not contain any duplicate values.");
    }
}