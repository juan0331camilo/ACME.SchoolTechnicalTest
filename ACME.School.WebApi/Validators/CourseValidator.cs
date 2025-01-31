namespace ACME.School.WebApi.Validators
{
    using ACME.School.Application.DTOs;
    using FluentValidation;

    public class CourseValidator : AbstractValidator<CourseDTO>
    {
        public CourseValidator()
        {
            RuleFor(model => model.CourseID).NotNull().WithMessage("The field CourseID is mandatory");
            RuleFor(model => model.Title).NotNull().WithMessage("The field Title is mandatory");
            RuleFor(model => model.RegistrationFee).NotNull().WithMessage("The field RegistrationFee is mandatory");
            RuleFor(model => model.StartDate).NotNull().WithMessage("The field StartDate is mandatory");
            RuleFor(model => model.EndDate).NotNull().WithMessage("The field EndDate is mandatory");
        }
    }
}
