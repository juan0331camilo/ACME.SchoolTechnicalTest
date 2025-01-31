namespace ACME.School.WebApi.Endpoints
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Services;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Minimal APIs Course
    /// </summary>
    public static class CourseEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/courses")
               .WithTags(new[] { "Courses" })
               .RequireAuthorization()
               .WithOpenApi();

            _ = root.MapGet("{startDate}/{endDate}", GetCoursesEnrollments)
                .WithName("Get Courses Enrollments")
                .WithDescription("Get Courses Enrollments Method")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status401Unauthorized)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            _ = root.MapPost("", CreateCourse)
                .WithName("Create Course")
                .WithDescription("Create Course Method")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status401Unauthorized)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static IResult GetCoursesEnrollments([FromServices] ICourseService courseService,
            [FromRoute] string startDate,
            [FromRoute] string endDate)
        {
            #region Validar condiciones previas

            if (!DateTime.TryParse(startDate, out DateTime startDateFilter) ||
                !DateTime.TryParse(endDate, out DateTime endDateFilter))
            {
                ResponseDTO error = new() { Message = "StartDate or EndDate Invalid Format (YYYY-MM-DD)" };
                return Results.BadRequest(error);
            }

            #endregion

            try
            {
                IList<CourseEnrollmentDTO> courseEnrollments = courseService.GetCoursesEnrollments(startDateFilter, endDateFilter);

                return Results.Ok(new ResponseDTO
                {
                    Data = courseEnrollments,
                    IsSuccess = true,
                    Message = "Courses Enrollments retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.InnerException?.Message) ? ex.Message : ex.InnerException?.Message;
                ResponseDTO error = new() { Message = $"{message}" };
                return Results.Json(error, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        static async Task<IResult> CreateCourse([FromServices] ICourseService courseService,
            [FromServices] IValidator<CourseDTO> validator,
            [FromBody] CourseDTO courseDTO)
        {
            #region Validar condiciones previas

            ValidationResult validationResult = await validator.ValidateAsync(courseDTO);
            if (!validationResult.IsValid)
            {
                ResponseDTO error = new() { Message = string.Join(", ", validationResult.Errors.Select(failure => $"Error: {failure.ErrorMessage}")) };
                return Results.BadRequest(error);
            }

            #endregion

            try
            {
                courseDTO = courseService.CreateCourse(courseDTO.CourseID,
                    courseDTO.Title,
                    courseDTO.Credits,
                    courseDTO.RegistrationFee,
                    courseDTO.StartDate,
                    courseDTO.EndDate);

                return Results.Ok(new ResponseDTO
                {
                    Data = courseDTO,
                    IsSuccess = true,
                    Message = "Course created successfully"
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
