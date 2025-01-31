namespace ACME.School.Test.Tests
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Services;
    using ACME.School.Domain.Data;
    using ACME.School.Domain.Entities;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    [TestClass]
    public class CourseServiceTests
    {
        readonly Mock<IMapper> _mapperMock;
        readonly CourseService _courseService;

        static async Task<ACMEContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ACMEContext>()
                .UseInMemoryDatabase(databaseName: "AcmeSchoolTestDB")
                .Options;
            var databaseContext = new ACMEContext(options);
            databaseContext.Database.EnsureCreated();

            if (!databaseContext.Enrollments.Any())
            {
                var startDate = DateTime.UtcNow;
                var endDate = DateTime.UtcNow.AddMonths(1);

                databaseContext.Courses.AddRange(new List<Course>
                {
                    new() { CourseID = 1000, Title = "Curso de Docker", Credits = 3, StartDate = startDate, EndDate = endDate },
                    new() { CourseID = 1001, Title = "Curso de Kubernetes", Credits = 4,StartDate = startDate, EndDate = endDate },
                    new() { CourseID = 1002, Title = "Curso de Linux", Credits = 5, StartDate = startDate, EndDate = endDate }
                });

                databaseContext.Students.AddRange(new List<Student>
                {
                    new() { ID = 1, FirstMidName = "John", LastName = "Doe" },
                });

                databaseContext.Enrollments.AddRange(new List<Enrollment>
                {
                    new() { CourseID = 1000, StudentID = 1, Grade = Grade.A },
                });

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        public CourseServiceTests()
        {
            var context = GetDatabaseContext().GetAwaiter().GetResult();

            _mapperMock = new Mock<IMapper>();

            // Crear instancia del servicio
            _courseService = new CourseService(_mapperMock.Object, context);
        }

        [TestMethod]
        public void CreateCourse_ShouldReturn_CourseDTO()
        {
            // Arrange
            var course = new Course
            {
                CourseID = 1003,
                Title = "Test Course",
                Credits = 3,
                RegistrationFee = 100.0m,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(3)
            };

            var courseDto = new CourseDTO
            {
                Title = course.Title,
                Credits = course.Credits,
                RegistrationFee = course.RegistrationFee,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };

            _mapperMock.Setup(m => m.Map<CourseDTO>(It.IsAny<Course>())).Returns(courseDto);

            // Act
            var result = _courseService.CreateCourse(course.CourseID,
                course.Title, 
                course.Credits, 
                course.RegistrationFee, 
                course.StartDate, 
                course.EndDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(course.Title, result.Title);
            Assert.AreEqual(course.Credits, result.Credits);
        }

        [TestMethod]
        public void GetCoursesEnrollments_ShouldReturn_ListOfCourseEnrollmentDTO()
        {
            // Arrange
            var startDate = DateTime.UtcNow.Date;
            var endDate = DateTime.UtcNow.AddMonths(1);

            var course = new Course
            {
                CourseID = 1000,
                Title = "Curso de Docker",
                Credits = 3,
                StartDate = startDate,
                EndDate = endDate
            };

            var student = new Student { ID = 1, FirstMidName = "John", LastName = "Doe" };

            var enrollments = new List<Enrollment>
            {
                new () { Course = course, Student = student }
            };

            var courseDto = new CourseDTO { Title = course.Title };
            var studentDto = new StudentDTO { FirstMidName = student.FirstMidName, LastName = student.LastName };
            var courseEnrollmentDto = new CourseEnrollmentDTO
            {
                Course = courseDto,
                Students = [studentDto]
            };

            _mapperMock.Setup(m => m.Map<CourseDTO>(It.IsAny<Course>())).Returns(courseDto);
            _mapperMock.Setup(m => m.Map<StudentDTO>(It.IsAny<Student>())).Returns(studentDto);

            // Act
            var result = _courseService.GetCoursesEnrollments(startDate, endDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Curso de Docker", result[0].Course.Title);
            Assert.AreEqual("John", result[0].Students[0].FirstMidName);
        }
    }
}