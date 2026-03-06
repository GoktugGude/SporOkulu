using FluentValidation;

namespace SporOkulu.Application;

public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentValidator()
    {
        RuleFor(s => s.FirstName)
            .NotEmpty().WithMessage("Öğrenci adı boş geçilemez.")
            .MaximumLength(50).WithMessage("Ad 50 karakterden uzun olamaz");
            
        RuleFor(s => s.LastName)
            .NotEmpty().WithMessage("Öğrenci soyadı boş geçilemez.");

        RuleFor(s => s.BirthDate)
            .LessThan(DateTime.Now).WithMessage("Doğum tarihi bugünden büyük olamaz.");
            
    }
}
