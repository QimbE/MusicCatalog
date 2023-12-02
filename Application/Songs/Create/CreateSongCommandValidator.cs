using Domain.Songs;
using FluentValidation;

namespace Application.Songs.Create;

public class CreateSongCommandValidator:AbstractValidator<CreateSongCommand>
{
    public CreateSongCommandValidator()
    {
        RuleFor(s => s.ReleaseId).NotEmpty();

        RuleFor(s => s.GenreId).NotEmpty();
            
        RuleFor(s => s.Name).NotEmpty().MaximumLength(150);

        RuleFor(s => s.AudioLink).NotEmpty().MaximumLength(2000);

        RuleFor(s => s.ArtistOnFeatIds).NotNull().Must(NotContainDuplicates).WithMessage("Must not contain any duplicate values.");
    }

    public static bool NotContainDuplicates(List<Guid> artistsOnFeatIds)
    {
        return artistsOnFeatIds.Count == artistsOnFeatIds.Distinct().Count();
    }
}