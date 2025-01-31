namespace ACME.School.WebApi.Endpoints
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Minimal APIs Enrollment
    /// </summary>
    public static class EnrollmentEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/enrollment")
               .WithTags(new[] { "Enrollment" })
               .RequireAuthorization()
               .WithOpenApi();

            _ = root.MapPost("", CreateEnrollment)
                .WithName("Create Enrollment")
                .WithDescription("Create Enrollment Method")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status401Unauthorized)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static async Task<IResult> CreateEnrollment([FromServices] IEnrollmentService enrollmentService,
            [FromServices] IValidator<EnrollmentDTO> validator,
            [FromBody] EnrollmentDTO enrollmentDTO)
        {
            #region Validar condiciones previas

            ValidationResult validationResult = await validator.ValidateAsync(enrollmentDTO);
            if (!validationResult.IsValid)
            {
                ResponseDTO error = new() { Message = string.Join(", ", validationResult.Errors.Select(failure => $"Error: {failure.ErrorMessage}")) };
                return Results.BadRequest(error);
            }

            #endregion

            try
            {
                enrollmentDTO = enrollmentService.AddEnrrolmentStudent(enrollmentDTO.StudentID,
                    enrollmentDTO.CourseID,
                    enrollmentDTO.Payment);

                return Results.Ok(new ResponseDTO
                {
                    Data = enrollmentDTO,
                    IsSuccess = true,
                    Message = "enrollment of student created successfully."
                });
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.InnerException?.Message) ? ex.Message : ex.InnerException?.Message;
                ResponseDTO error = new() { Message = $"{message}" };
                return Results.Json(error, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
