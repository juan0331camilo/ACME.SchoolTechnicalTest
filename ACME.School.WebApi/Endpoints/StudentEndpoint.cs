namespace ACME.School.WebApi.Endpoints
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Minimal APIs Student
    /// </summary>
    public static class StudentEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/students")
               .WithTags(new[] { "Students" })
               .RequireAuthorization()
               .WithOpenApi();

            _ = root.MapPost("", CreateStudent)
                .WithName("Create Student")
                .WithDescription("Create Student Method")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status401Unauthorized)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static async Task<IResult> CreateStudent([FromServices] IStudentService studentService,
            [FromServices] IValidator<StudentDTO> validator,
            [FromBody] StudentDTO studentDTO)
        {
            #region Validar condiciones previas

            ValidationResult validationResult = await validator.ValidateAsync(studentDTO);
            if (!validationResult.IsValid)
            {
                ResponseDTO error = new() { Message = string.Join(", ", validationResult.Errors.Select(failure => $"Error: {failure.ErrorMessage}")) };
                return Results.BadRequest(error);
            }

            #endregion

            try
            {
                studentDTO = studentService.CreateStudent(studentDTO.FirstMidName,
                    studentDTO.LastName,
                    studentDTO.BirthDate);

                return Results.Ok(new ResponseDTO
                {
                    Data = studentDTO,
                    IsSuccess = true,
                    Message = "Student created successfully."
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
