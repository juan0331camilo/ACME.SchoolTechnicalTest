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
    public class StudentServiceTests
    {
        readonly Mock<IMapper> _mapperMock;
        readonly StudentService _studentService;

        static ACMEContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ACMEContext>()
                .UseInMemoryDatabase(databaseName: "AcmeSchoolTestDB")
                .Options;
            var databaseContext = new ACMEContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        public StudentServiceTests()
        {
            var context = GetDatabaseContext();

            _mapperMock = new Mock<IMapper>();

            // Crear instancia del servicio
            _studentService = new StudentService(_mapperMock.Object, context);
        }

        [TestMethod]
        public void CreateStudent_ShouldReturn_StudentDTO()
        {
            // Arrange
            var birthDate = DateTime.UtcNow.AddYears(-18); // Aseguramos que tiene 18 años
            var student = new Student
            {
                FirstMidName = "John",
                LastName = "Doe",
                BirthDate = birthDate,
                EnrollmentDate = DateTime.UtcNow
            };

            var studentDto = new StudentDTO
            {
                FirstMidName = student.FirstMidName,
                LastName = student.LastName,
                BirthDate = student.BirthDate
            };

            _mapperMock.Setup(m => m.Map<StudentDTO>(It.IsAny<Student>())).Returns(studentDto);

            // Act
            var result = _studentService.CreateStudent(student.FirstMidName, student.LastName, student.BirthDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(student.FirstMidName, result.FirstMidName);
            Assert.AreEqual(student.LastName, result.LastName);
        }

        [TestMethod]
        public void CreateStudent_ShouldThrowException_WhenStudentIsUnderage()
        {
            // Arrange
            var birthDate = DateTime.UtcNow.AddYears(-17); // Menor de edad

            // Act & Assert
            var exception = Assert.ThrowsException<Exception>(() =>
                _studentService.CreateStudent("John", "Doe", birthDate)
            );

            Assert.AreEqual("debes ser mayor de edad.", exception.Message);
        }
    }
}