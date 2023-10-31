using FluentValidation;

namespace Application.Artists.Update;

public class UpdateArtistCommandValidator: AbstractValidator<UpdateArtistCommand>
{
    public UpdateArtistCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Description)
            .MaximumLength(2000);
    }
}