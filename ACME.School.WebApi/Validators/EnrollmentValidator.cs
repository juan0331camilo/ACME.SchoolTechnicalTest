using ACME.School.Application.DTOs;
using FluentValidation;

namespace ACME.School.WebApi.Validators
{
    public class EnrollmentValidator : AbstractValidator<EnrollmentDTO>
    {
        public EnrollmentValidator()
        {
            RuleFor(model => model.StudentID).NotNull().WithMessage("The field Title is mandatory");
            RuleFor(model => model.CourseID).NotNull().WithMessage("The field RegistrationFee is mandatory");
            RuleFor(model => model.Payment).NotNull().WithMessage("The field EndDate is mandatory");
        }
    }
}
