namespace ACME.School.WebApi.Validators
{
    using ACME.School.Application.DTOs;
    using FluentValidation;

    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(model => model.UserId).NotNull().WithMessage("The field UserId is mandatory");
            RuleFor(model => model.Password).NotNull().WithMessage("The field Password is mandatory");
        }
    }
}
