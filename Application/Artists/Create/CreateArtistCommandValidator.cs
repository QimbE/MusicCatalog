using FluentValidation;

namespace Application.Artists.Create;

public class CreateArtistCommandValidator: AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Description)
            .MaximumLength(2000);
    }
}