using System;
using FluentValidation;

namespace SporOkulu.Application.Validators.Student;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty().WithMessage("Güncellenecek öğrencinin ID bilgisi zorunludur.")
            .GreaterThan(0).WithMessage("Geçersiz öğrenci ID.");

        RuleFor(s => s.FirstName)
            .NotEmpty().WithMessage("Ad zorunludur.")
            .MaximumLength(50);

        RuleFor(s => s.LastName)
            .NotEmpty().WithMessage("Soyad zorunludur.");
    }
}
