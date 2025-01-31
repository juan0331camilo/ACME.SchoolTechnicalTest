namespace ACME.School.Application.Services
{
    using ACME.School.Application.DTOs;

    public interface ICourseService
    {
        CourseDTO CreateCourse(int courseID,
            string title,
            int credits,
            decimal registrationFee,
            DateTime startDate,
            DateTime endDate);

        IList<CourseEnrollmentDTO> GetCoursesEnrollments(DateTime startDate,
            DateTime endDate);
    }
}
