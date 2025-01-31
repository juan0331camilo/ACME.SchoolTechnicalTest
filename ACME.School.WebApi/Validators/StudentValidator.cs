namespace ACME.School.WebApi.Validators
{
    using ACME.School.Application.DTOs;
    using FluentValidation;

    public class StudentValidator : AbstractValidator<StudentDTO>
    {
        public StudentValidator()
        {
            RuleFor(model => model.FirstMidName).NotNull().WithMessage("The field FirstMidName is mandatory");
            RuleFor(model => model.LastName).NotNull().WithMessage("The field LastName is mandatory");
            RuleFor(model => model.BirthDate).NotNull().WithMessage("The field BirthDate is mandatory");
        }
    }
}
