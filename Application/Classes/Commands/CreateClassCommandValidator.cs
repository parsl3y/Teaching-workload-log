using FluentValidation;

namespace Application.Classes.Commands;

public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(x => x.ClassName)
            .NotEmpty();
        
        RuleFor(x => x.TotalClassNumber)
            .NotNull()
            .GreaterThan(0);
        
        RuleFor(x => x.CLassNumberToday)
            .NotNull()
            .LessThan(x => x.TotalClassNumber);

    }
}