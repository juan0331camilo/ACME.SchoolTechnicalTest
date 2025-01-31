namespace ACME.School.Application.Services
{
    using ACME.School.Application.DTOs;
    using ACME.School.Domain.Data;
    using ACME.School.Domain.Entities;
    using AutoMapper;

    public class CourseService(IMapper mapper,
        ACMEContext ACMEContext) : ICourseService
    {
        private readonly UnitOfWork unitOfWork = new(ACMEContext);

        public CourseDTO CreateCourse(int courseID,
            string title,
            int credits,
            decimal registrationFee,
            DateTime startDate,
            DateTime endDate)
        {
            Course course = new()
            {
                CourseID = courseID,
                Credits = credits,
                Title = title,
                RegistrationFee = registrationFee,
                StartDate = startDate,
                EndDate = endDate
            };
            unitOfWork.Repository<Course>().Insert(course);
            unitOfWork.Save();
            return mapper.Map<CourseDTO>(course);
        }

        public IList<CourseEnrollmentDTO> GetCoursesEnrollments(DateTime startDate,
            DateTime endDate)
        {

            IList<Enrollment> enrollments = unitOfWork.Repository<Enrollment>().Get(includeProperties: "Student,Course")
                        .Select(e => new
                        {
                            Enrollment = e,
                            StartDateOnly = e.Course.StartDate.Date,
                            EndDateOnly = e.Course.EndDate.Date
                        })
                        .Where(x => x.StartDateOnly <= startDate.Date && x.EndDateOnly >= endDate.Date)
                        .Select(x => x.Enrollment)
                        .ToList();

            List<CourseEnrollmentDTO> courseEnrollments = enrollments
                .GroupBy(e => e.Course)
                .Select(g => new CourseEnrollmentDTO
                {
                    Course = mapper.Map<CourseDTO>(g.Key),
                    Students = g.Select(e => mapper.Map<StudentDTO>(e.Student)).ToList()
                }).ToList();

            return courseEnrollments;
        }
    }
}
