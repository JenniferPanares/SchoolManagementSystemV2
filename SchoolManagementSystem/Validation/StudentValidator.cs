using FluentValidation;
using SchoolManagementSystem.Models;

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(student => student.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(student => student.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(student => student.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

           
    }
}
