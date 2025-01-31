namespace ACME.School.Application.DTOs
{
    public class CourseEnrollmentDTO
    {
        public CourseDTO Course { get; set; }

        public List<StudentDTO> Students { get; set; }
    }
}
